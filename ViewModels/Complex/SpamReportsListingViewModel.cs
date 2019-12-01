using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Complex
{
    public class SpamReportsListingViewModel
    {
        public IEnumerable<SpamReportViewModel> Reports { get; set; }
    }
}