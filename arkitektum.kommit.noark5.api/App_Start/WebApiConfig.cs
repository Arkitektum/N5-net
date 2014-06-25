using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var cors = new EnableCorsAttribute("http://localhost:64708", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
