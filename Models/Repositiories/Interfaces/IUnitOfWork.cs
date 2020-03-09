using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models.Repositiories;

namespace Translate.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        //repo 1
        IQuestionRepository Questions { get; }
        IAnswerRepository Answers { get; }
        IUsersRepository Users { get; }
        IVoteRepository Votes { get; }


        //repo 2

        void SaveChanges();
    }
}
