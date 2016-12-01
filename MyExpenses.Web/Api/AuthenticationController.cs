using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace MyExpenses.Web.Api
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("api/authenticate")]
        [AllowAnonymous]
        public String Authenticate(string user, string password)
        {
            var accessToken = "failed";

            if (!string.IsNullOrEmpty(user) && string.IsNullOrEmpty(password))
            {
                //var userIdentity = UserManager.FindAsync(user, password).Result;
                //if (userIdentity != null)
                var userId = Guid.NewGuid();
                if (user == "admin" && password == "admin")
                {
                    var identity = new ClaimsIdentity(Common.Authentication.OAuthBearerOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
                    AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                    var currentUtc = new SystemClock().UtcNow;
                    ticket.Properties.IssuedUtc = currentUtc;
                    ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
                    accessToken = Common.Authentication.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
                }
            }

            return accessToken;
        }
    }
}