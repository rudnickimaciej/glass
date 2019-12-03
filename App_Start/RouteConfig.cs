using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Translate
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.LowercaseUrls = true;

            routes.MapRoute(
             name: "Questions",
             url: "questions/",
             defaults: new { controller = "Forum", action = "Questions", langFrom = UrlParameter.Optional, langTo = UrlParameter.Optional }
            );


            routes.MapRoute(
             name: "Question",
             url: "questions/{langFrom}-{langTo}/{questionId}",
             defaults: new { controller = "Forum", action = "Question", questionId= UrlParameter.Optional, langFrom = UrlParameter.Optional, langTo = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "AddQuestion",
                url: "questions/add",
              defaults: new { controller = "Forum", action = "AddQuestion" }
            );

            routes.MapRoute(
             name: "EditQuestion",
               url: "questions/{langFrom}-{langTo}/{questionId}/edit",
             defaults: new { controller = "Forum", action = "EditQuestion", questionId = UrlParameter.Optional, langFrom = UrlParameter.Optional, langTo = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "CreateAnswer",
             url: "questions/{langFrom}-{langTo}/{questionId}/reply",
             defaults: new { controller = "Forum", action = "CreateAnswer", questionId = UrlParameter.Optional, langFrom = UrlParameter.Optional, langTo = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "Profile",
           url: "profiles/{userName}",
           defaults: new { controller = "Profile", action = "Details", userName = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "Spam",
            url: "admin/spam",
            defaults: new { controller = "Spam", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forum", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
