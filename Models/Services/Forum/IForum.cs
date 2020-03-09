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

        //Questions
        Question GetQuestion(int questionId);
        IEnumerable<Question> GetQuestions(string query);
        IEnumerable<Question> GetQuestions(string query, int currentPage, int pageSize);
        IEnumerable<Question> GetLatestQuestions(int amount);
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<Question> GetUserQuestions(string userId);
        IEnumerable<Answer> GetUserAnswers(string userId);


        int GetAllQuestionsCount();
        int GetAllQuestionsMatchingQueryCount(string query);
        Question AddQuestion(Question question);
        Question EditQuestion(EditQuestionFields editQuestion);
        void DeleteQuestion(int questionId);

        //Answers
        Answer GetAnswerById(int answerId);
        void AddAnswer(Answer answer);
        void EditAnswer(int answerId, string newContent);
        void DeleteAnswer(int answerId);

    }
}
