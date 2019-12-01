using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Complex
{
    public class AllQuestionsOfForumViewModel
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public string SearchQuery { get; set; }
    }
}