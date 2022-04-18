using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AfluexFollowUpDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/WebAPI/{id}",
                defaults: new { controller = "WebAPI", id = RouteParameter.Optional }
            );
        }
    }
}
