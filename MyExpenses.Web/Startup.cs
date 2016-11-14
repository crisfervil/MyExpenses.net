using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using MyExpenses.Domain;
using MyExpenses.Data.EF;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(MyExpenses.Web.Startup))]

namespace MyExpenses.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Configure Dependency injection
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IExpensesDataContext, ExpensesDataContext>(Lifestyle.Scoped);
            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Configure routes
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.XmlFormatter.UseXmlSerializer = true;
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

            app.UseWebApi(config);
        }
    }
}
