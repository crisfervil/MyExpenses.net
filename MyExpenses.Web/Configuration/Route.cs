using System.Web.Http;
using System.Web.Mvc;

namespace MyExpenses.Web.Configuration
{
    public class Route
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            var routes = config.Routes;
            routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
