using MyExpenses.Data.EF;
using MyExpenses.Domain;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyExpenses.Web
{
    public class DependencyInjectionConfig
    {
        public static void Config()
        {

            // 1. Create a new Simple Injector container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // 2. Configure the container (register)
            container.Register<IExpensesDataContext, ExpensesDataContext>(Lifestyle.Scoped);
            //container.Register(typeof(ApplicationUserManager), () => null, Lifestyle.Transient);
            //container.Register(typeof(ApplicationSignInManager), () => null, Lifestyle.Transient);

            //container.GetRegistration(typeof(ApplicationUserManager)).Registration.SuppressDiagnosticWarning();

            // 3. Optionally verify the container's configuration.
            container.Verify();

            // 4. Register the container as MVC3 IDependencyResolver.
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}