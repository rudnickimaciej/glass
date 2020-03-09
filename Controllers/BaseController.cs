using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models;
using Translate.Models.Services;
using Translate.Repositories;
using Translate.ViewModels.Builders;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IApplicationUser _userService;
        protected IForum _forumService;
        protected IViewModelBuilder _builder;
        protected IUnitOfWork _uow;

        public BaseController(IApplicationUser userService, IForum forumService, IViewModelBuilder builder)
        {
            _userService = userService;
            _forumService = forumService;
            _builder = builder;
            _uow = new UnitOfWork(new ApplicationDbContext());
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           //var langs= _forumService.GetAllLanguages();
           //var langsViewModels= langs.Select(l => new LanguageViewModel()
           // {
           //     Name = l.Name,
           //     Abbreviation = l.Abbreviation,
           //     Id = l.Id
           // });

           // ViewBag.Languages = langsViewModels;

           // base.OnActionExecuting(filterContext);
        }
    }
}