using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Services;
using Translate.Models.Services.Spam;
using Translate.ViewModels.Builders;
using Translate.ViewModels.Complex;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{

    public class SpamController : BaseController
    {
        protected readonly ISpam _spamService = new SpamService();



        public SpamController(IApplicationUser userService, IForum forumService, IViewModelBuilder builder) : base(userService, forumService, builder)
        {
        }



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