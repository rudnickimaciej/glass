using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models;
using Translate.Models.Services;
using Translate.ViewModels.Complex;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = BuildHomeIndexViewModel();

            return View(model);
        }

        private HomeIndexViewModel BuildHomeIndexViewModel()
        {
            var latest = _forumService.GetLatestQuestions(10);

           var questionViews = latest.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Title = q.Title,
                DatePosted = q.Created,
                AnswersCount = q.Answers.Count,
                LanguageFrom = _builder.BuildLanguage(q.LanguageFrom),
                LanguageTo = _builder.BuildLanguage(q.LanguageTo)
                
            });
           


            return new HomeIndexViewModel { LatestQuestions = questionViews, SearchQuery=""};
        }
    }
}