using Microsoft.Owin;
using Microsoft.Owin.Host;
using MyExpenses.Data.EF;
using MyExpenses.Domain;
using Newtonsoft.Json;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using MyExpenses.Web.Common;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;

[assembly: OwinStartup(typeof(MyExpenses.Web.Startup))]
namespace MyExpenses.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Configure Dependency injection
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IExpensesDataContext, ExpensesDataContext>(Lifestyle.Scoped);
            container.Register<IAuthRepository, AuthRepository>(Lifestyle.Scoped);
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuth();
            app.UseWebApi(config);

            // configure static files
            var staticFilesOptions = new FileServerOptions() {
#if DEBUG
                EnableDirectoryBrowsing=true,
#endif
                FileSystem=new PhysicalFileSystem(@".\Public")
            };
            app.UseFileServer(staticFilesOptions);

            // Configure routes (The routes are going to be defined in the controllers as attributes)
            config.MapHttpAttributeRoutes();

            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
        }
    }
}
