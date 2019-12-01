using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels;

namespace Translate.Models
{
    public class SpamReport
    {
        public int Id { get; set; }
        public virtual SpamReason SpamReason { get; set; }
        public DateTime Created { get; set; }
        public virtual Answer ReportedAnswer { get; set; }
        public virtual ApplicationUser ReportingUser { get; set; }
    }
}