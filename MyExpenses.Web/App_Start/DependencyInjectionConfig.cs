using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyExpenses.Data.EF;
using MyExpenses.Domain;
using MyExpenses.Web.Models;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyExpenses.Web
{
    public class DependencyInjectionConfig
    {
        public static void Config(IAppBuilder app)
        {

            // 1. Create a new Simple Injector container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // 2. Configure the container (register)

            // https://simpleinjector.codeplex.com/discussions/564822
            container.RegisterSingleton<IAppBuilder>(app);
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationDbContext>(()=> new ApplicationDbContext("DefaultConnection"));

            container.RegisterPerWebRequest<SignInManager<ApplicationUser, string>, ApplicationSignInManager>();
            container.RegisterPerWebRequest<IAuthenticationManager>(()=>
                AdvancedExtensions.IsVerifying(container)
                    ? new OwinContext(new Dictionary<string,object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication);

            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));

            container.RegisterInitializer<ApplicationUserManager>(manager => manager.DefaultConfig(app,manager));

            container.Register<IExpensesDataContext, ExpensesDataContext>(Lifestyle.Scoped);

            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // 3. Optionally verify the container's configuration.
            container.Verify();

            // 4. Register the container as MVC3 IDependencyResolver.
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}