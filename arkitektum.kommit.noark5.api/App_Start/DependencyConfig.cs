using arkitektum.kommit.noark5.api.Services;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace arkitektum.kommit.noark5.api.App_Start
{
    public class DependencyConfig
    {
        

        public static void Configure(IAppBuilder app)
        {
            ConfigureDependenciesForAPI(app);
        }

        private static void ConfigureDependenciesForAPI(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            MockNoarkDatalayer datalayer;
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            datalayer = new MockNoarkDatalayer();
            builder.RegisterInstance(datalayer);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}