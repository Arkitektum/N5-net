using CacheCow.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.OData.Extensions;
using System.Web.OData;
using WebApi.Hal;

namespace arkitektum.kommit.noark5.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigApi(config);
            ConfigOdata(config);

        }
        private static void ConfigApi(HttpConfiguration config)
        {
            // Web API configuration and services
            

            // Web API routes
            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.noark5-v4+json"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.noark5-v4+xml"));
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            config.Formatters.XmlFormatter.MaxDepth = 5;
            //config.Formatters.XmlFormatter.WriterSettings.ConformanceLevel= System.Xml.ConformanceLevel.
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            var cachecow = new CachingHandler(config);
            GlobalConfiguration.Configuration.MessageHandlers.Add(cachecow);

        }

        private static void ConfigOdata(HttpConfiguration config)
        {
            //var controllerSelector = new ODataVersionControllerSelector(config);
            //config.Services.Replace(typeof(IHttpControllerSelector), controllerSelector);

            //// Define a versioned route
            //config.MapODataServiceRoute("V1RouteVersioning", "odata/v1");
            //controllerSelector.RouteVersionSuffixMapping.Add("V1RouteVersioning", "V1");
            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Book>("Books");
            //builder.EntitySet<Customer>("Customers");
            //var model = builder.GetEdmModel();

            //config.MapODataServiceRoute(
            //    routeName: "ODataRoute",
            //    routePrefix: null,
            //    model: model);
        }

    }
}
