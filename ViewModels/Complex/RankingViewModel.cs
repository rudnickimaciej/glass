using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Complex
{
    public class RankingViewModel: PaginationBaseViewModel
    {
        public  IEnumerable<ProfileViewModel> Users { get; set; }
        public IEnumerable<QuestionViewModel> NewQuestions { get; set; }
    }
}