using MyExpenses.Web.Configuration;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace MyExpenses.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            WebApi.Configure(config);
            DependencyInjection.Configure(config);
            Route.RegisterRoutes(config);
            appBuilder.UseWebApi(config);

        }
    }
}
