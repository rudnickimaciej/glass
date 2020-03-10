using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IEnumerable<Answer> GetTop(int count)
        {
            //TODO: Top Questions 
            return Context.Answers.OrderBy(a => a.Created).Take(count);
        }

        public int GetCount(string userId)
        {
            return Context.Answers.Where(a => a.User.Id == userId).Count();
        }
        
        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}