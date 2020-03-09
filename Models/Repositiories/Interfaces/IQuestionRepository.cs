using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models;

namespace Translate.Repositories
{
    public interface IQuestionRepository:IRepository<Question>
    {
        //methods specific for this repository

        int GetAllQuestionsCount();
        int GetAllQuestionsMatchingQueryCount(string query);
        IList<Question> GetNewQuestions(int count);
        IList<Question> GetQuestionsWithPagination(string searchQuery, int page, int questionsPerPage);
    }
}
