using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JYSpaCinema.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //认证MesageHandler
            config.MessageHandlers.Add(new JYSpaCinema.Web.Infrastructure.MessageHandlers.JYSpaCinemaAuthHandler());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
