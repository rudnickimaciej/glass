using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.Models;

namespace Translate.ViewModels.Components
{
    public class SpamReportViewModel
    {
        public int SpamReasonId { get; set; }
        public string SpamReasonContent { get; set; }
        public DateTime Created { get; set; }

        public AnswerViewModel ReportedAnswer { get; set; }
        public int QuestionId { get; set; }
        public string ReportingUserId { get; set; }
        public string ReportingUserName { get; set; }

    }
}