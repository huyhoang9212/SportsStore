using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // /
            routes.MapRoute(null,
                "", new { controller = "Product", action = "List", category = (string)null, page = 1 });

            // /page-2
            routes.MapRoute(
                name: null,
                url: "page-{page}",
                defaults: new { Controller = "Product", action = "List", category = (string)null },
                constraints: new { page = @"\d+" }
            );

            // /Soccer
            routes.MapRoute(null,
                url: "{category}",
                defaults: new { controller="Product",action="List", page=1});

            // /Soccer/page-2
            routes.MapRoute(
                name: null,
                url: "{category}/page-{page}",
                defaults: new { controller = "Product", action = "List" },
                constraints: new { page=@"\d+"}
            );

            // default
            routes.MapRoute(null,
                url: "{controller}/{action}");
        }
    }
}
