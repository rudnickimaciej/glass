using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Services;
using Translate.ViewModels.Builders;
using Translate.ViewModels.Complex;

namespace Translate.Controllers
{
    public class TestController : BaseController
    {
        public TestController(IApplicationUser userService, IForum forumService,IViewModelBuilder builder) : base(userService, forumService, builder)
        {
        }

        public ActionResult Index()
        {
            using (_uow)
            {
                var questions = _uow.Questions.GetAll();
            }
            return View("~/Views/Home/Index.cshtml");
        }


        public ActionResult Questions(string searchQuery)
        {
            var questions = _forumService.GetQuestions(searchQuery).ToList();
            if (searchQuery != null)
            {
                questions.RemoveAll(q => !q.Content.Contains(searchQuery));
            }
            var questionListing = questions.Select(q => _builder.BuildQuestion(q)).ToList();

            var model = new AllQuestionsOfForumViewModel
            {
                Questions = questionListing,

            };
            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}