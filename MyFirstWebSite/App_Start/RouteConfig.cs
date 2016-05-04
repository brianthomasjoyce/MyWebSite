using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyFirstWebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //prevent ppl to access certain files

            //        routes.MapRoute(
            //         name: "Home",
            //         url: "Home",
            //         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //
            //);

            //HomePage Intro
            routes.MapRoute(
                name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Index", action = "HomePage", id = UrlParameter.Optional }
            );
        }
    }
}
