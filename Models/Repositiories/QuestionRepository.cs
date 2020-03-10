using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models;

namespace Translate.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {

        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public int GetAllQuestionsCount()
        {
            return Context.Questions.Count();
        }

        public int GetAllQuestionsMatchingQueryCount(string query)
        {
            if (query != null)
                return Context.Questions.Where(q => q.Content.Contains(query)).Count();
            else return GetAllQuestionsCount();
        }

        public IList<Question> GetNewQuestions(int count)
        {
            return Context.Questions.OrderBy(q => q.Created).Take(count).ToList();
        }

        public IList<Question> GetQuestionsWithPagination(string searchQuery, int page, int questionsPerPage)
        {
            if (searchQuery != null)
                return Context.Questions.Where(q => q.Content.Contains(searchQuery)).OrderByDescending(q => q.Created).Skip((page - 1) * questionsPerPage).Take(questionsPerPage).ToList();
            else
                return Context.Questions.OrderByDescending(q => q.Created).Skip((page - 1) * questionsPerPage).Take(questionsPerPage).ToList();
        }
        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }

    }
}
