using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UoUWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "About",
                url: "About/{id}",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Contact",
                url: "Contact/{id}",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Dashboard",
                url: "Dashboard/{id}",
                defaults: new { controller = "Index", action = "Dashboard", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
