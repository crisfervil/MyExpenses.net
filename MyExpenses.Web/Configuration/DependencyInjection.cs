using MyExpenses.Data.EF;
using MyExpenses.Domain;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Http;

namespace MyExpenses.Web.Configuration
{
    public class DependencyInjection
    {
        public static void Configure(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IExpensesDataContext, ExpensesDataContext>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            config. = (System.Web.Mvc.IDependencyResolver)new SimpleInjectorDependencyResolver(container);
        }
    }
}
