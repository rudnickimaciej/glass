using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Translate
{
    public class RouteConfig3
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ForumTwoLanguages",
               url: "forums/{langFrom}-{langTo}",
               defaults: new { controller = "Forum", action = "OneForum", langFrom = UrlParameter.Optional, langTo = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "ForumsFromLanguage",
              url: "forums/{langFrom}-",
              defaults: new { controller = "Forum", action = "SomeForums", langFrom = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "ForumsToLanguage",
              url: "forums/-{langTo}",
              defaults: new { controller = "Forum", action = "SomeForums", langTo = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "QuestionsOfForum",
            url: "forums/{langFrom}-{langTo}/questions",
            defaults: new { controller = "Forum", action = "Questions" }
          );

            routes.MapRoute(
            name: "Question",
            url: "forums/{langFrom}-{langTo}/questions/{questionId}",
            defaults: new { controller = "Forum", action = "Question" }
          );

            routes.MapRoute(
            name: "AddQuestion",
            url: "questions/add",
            defaults: new { controller = "Forum", action = "Question" }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forum", action = "AllForums", id = UrlParameter.Optional }
            );

        }
    }
}
