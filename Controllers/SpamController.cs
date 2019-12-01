using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Services.Spam;
using Translate.ViewModels.Complex;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{

    public class SpamController : BaseController
    {
        protected readonly ISpam _spamService = new SpamService();
        
        // GET: admin/spams
        public ActionResult Index()
        {
           var reports= _spamService.GetAllReports();
            var reportsViewModels = reports.Select(r => new SpamReportViewModel()
            {
              Created = r.Created,
              SpamReasonId =r.SpamReason.Id,
              SpamReasonContent =r.SpamReason.Content,
              ReportedAnswer = _builder.BuildAnswer(r.ReportedAnswer),
              QuestionId = r.ReportedAnswer.Question.Id,
              LanguageFrom = _builder.BuildLanguage(r.ReportedAnswer.Question.LanguageFrom),
              LanguageTo = _builder.BuildLanguage(r.ReportedAnswer.Question.LanguageTo),
              ReportingUserId = r.ReportingUser.Id,
              ReportingUserName = r.ReportingUser.UserName,

            });

            var model = new SpamReportsListingViewModel()
            {
                Reports = reportsViewModels
            };

            var reasons = _spamService.GetAllReasons();
            var reasonViewModels = reasons.Select(r => new SpamReasonViewModel()
            {
              Id = r.Id,
              Content=r.Content
            });

            ViewBag.SpamReasons = reasonViewModels;
            return View(model);
        }
    }
}