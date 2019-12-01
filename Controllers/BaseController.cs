using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Services;
using Translate.ViewModels.Builders;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApplicationUser _userService = new ApplicationUserService();
        protected IForum _forumService = new ForumService();
        protected ViewModelBuilder _builder = new ViewModelBuilder();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           var langs= _forumService.GetAllLanguages();
           var langsViewModels= langs.Select(l => new LanguageViewModel()
            {
                Name = l.Name,
                Abbreviation = l.Abbreviation,
                Id = l.Id
            });

            ViewBag.Languages = langsViewModels;

            base.OnActionExecuting(filterContext);
        }
    }
}