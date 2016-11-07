﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyExpenses.Web.Startup))]

namespace MyExpenses.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyInjectionConfig.Config(app);
            ConfigureAuth(app);
        }
    }
}
