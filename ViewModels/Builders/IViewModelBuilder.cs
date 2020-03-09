using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models;
using Translate.ViewModels.Complex;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Builders
{
    public interface IViewModelBuilder
    {
         QuestionViewModel BuildQuestion(Question question);
         AnswerViewModel BuildAnswer(Answer answer);

         ReplyViewModel BuildReplyViewModel(Question question, ApplicationUser user);
         EditAnswerViewModel BuilEditAnswerViewModel(Answer answer, ApplicationUser user);

         ProfileViewModel BuilProfileViewModel(ApplicationUser user);

    }
}

