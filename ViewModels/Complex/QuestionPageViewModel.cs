using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;


namespace Translate.ViewModels.Complex
{
    public class QuestionPageViewModel
    {
        public QuestionViewModel Question { get; set; } 
        public IEnumerable<AnswerViewModel> Answers { get; set; }
        public bool IsAdmin { get; set; }
        public string ActiveUserId { get; set; }

    }
}