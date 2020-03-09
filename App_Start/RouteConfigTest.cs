using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Translate
{
    public class RouteConfigTest
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          
            routes.MapRoute(
            name: "",
            url: "users/{userName}",
            defaults: new { controller = "Profile", action = "UserProfile" }
            );
   


            routes.MapRoute(
            name: "",
            url: "questions/",
            defaults: new { controller = "Forum", action = "Questions" }
           );
                    routes.MapRoute(
            name: "AddQuestion",
              url: "questions/add",
            defaults: new { controller = "Forum", action = "AddQuestion" }
          );
            routes.MapRoute(
           name: "",
           url: "questions/{questionId}",
           defaults: new { controller = "Forum", action = "Question", questionId = UrlParameter.Optional }
          );

        

            routes.MapRoute(
           name: "CreateAnswer",
           url: "questions/{questionId}/reply",
           defaults: new { controller = "Forum", action = "CreateAnswer", questionId = UrlParameter.Optional }
          );

                    routes.MapRoute(
          name: "Ranking",
            url: "ranking",
          defaults: new { controller = "Forum", action = "Ranking", active = UrlParameter.Optional }
        );

                    routes.MapRoute(
         name: "Register",
           url: "account/register",
         defaults: new { controller = "Account", action = "Register" }
        );

            routes.MapRoute(
     name: "Login",
       url: "account/login",
     defaults: new { controller = "Account", action = "Login" }
    );


            routes.MapRoute(
     name: "Test",
       url: "test/all",
     defaults: new { controller = "Test", action = "Index" }
    );

            routes.MapRoute(
                   name: "Default",
                   url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Forum", action = "Index", id = UrlParameter.Optional }
               );


        }
    }
}
