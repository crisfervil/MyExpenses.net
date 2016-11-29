using Microsoft.Owin;
using MyExpenses.Data.EF;
using MyExpenses.Domain;
using Newtonsoft.Json;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

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
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.UseWebApi(config);

            // Configure routes
            config.MapHttpAttributeRoutes();

            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
        }
    }
}
