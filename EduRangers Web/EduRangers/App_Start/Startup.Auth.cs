using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using EduRangers.Providers;
using EduRangers.Models;
using BL.Infrastructures;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using SimpleInjector;

[assembly: OwinStartup(typeof(EduRangers.App_Start.Startup))]

namespace EduRangers.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);

            var container = new Container();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            IoCResolver.Load(container);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            container.Verify();
            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
        }
    }
}
