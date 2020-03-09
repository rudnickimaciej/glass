
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models;
using Translate.Models.Repositiories;

namespace Translate.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            Questions = new QuestionRepository(context);
            Answers = new AnswerRepository(context);
            Users = new UserRepository(context);
            Votes = new VoteRepository(context);
            _context = context;
        }

        public IQuestionRepository Questions { get; private set; }
        public IAnswerRepository Answers { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IVoteRepository Votes { get; private set; }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
