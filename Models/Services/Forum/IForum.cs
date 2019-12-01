using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate.Models.Services
{
  public interface IForum
    {

        //Languages
        IEnumerable<Language> GetAllLanguages();
        Language GetLanguage(string abbr);

        //Questions
        Question GetQuestion(int questionId);
        IEnumerable<Question> GetQuestions(string langFrom, string langTo);
        IEnumerable<Question> GetLatestQuestions(int amount);
        IEnumerable<Question> GetAllQuestions();
        Question AddQuestion(Question question);
        Question EditQuestion(EditQuestionFields editQuestion);
        void DeleteQuestion(int questionId);

        //Answers
        Answer GetAnswerById(int answerId);
        void AddAnswer(Answer answer);
        void EditAnswer(int answerId, string newContent);
        void DeleteAnswer(int answerId);

        //Reply
         //void AddReply(int questionId);

        //Users
        //IEnumerable<ApplicationUser> GetAllActiveUsers();

    }
}
