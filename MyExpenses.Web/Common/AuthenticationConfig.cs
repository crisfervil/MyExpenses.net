using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace MyExpenses.Web.Common
{
    public static class AuthenticationConfig
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static void UseOAuth(this IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new MyAuthorizationServerProvider()
            };

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
            {
                // Configure additional options here    
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            //This will used the HTTP header: "Authorization"      Value: "Bearer 1234123412341234asdfasdfasdfasdf"
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

        }
    }
}