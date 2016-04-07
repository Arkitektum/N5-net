using CacheCow.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace arkitektum.kommit.noark5.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
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
            config.Formatters.XmlFormatter.UseXmlSerializer = false;
            //config.Formatters.JsonFormatter.SerializerSettings.Re‌ferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            var cachecow = new CachingHandler(config);
            GlobalConfiguration.Configuration.MessageHandlers.Add(cachecow);
        }
    }
}
