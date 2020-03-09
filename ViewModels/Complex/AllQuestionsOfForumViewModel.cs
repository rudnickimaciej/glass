using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Complex
{
    public class AllQuestionsOfForumViewModel : PaginationBaseViewModel
    {
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public int QuestionsMatchingQueryCount { get; set; }
        public string SearchQuery { get; set; }
 
        public IEnumerable<AnswerViewModel> TopAnswers { get; set; }
        public StatisticsViewModel Statistics { get; set; }

    }
}