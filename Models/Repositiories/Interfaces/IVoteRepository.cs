using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models.Domain;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
  public  interface IVoteRepository:IRepository<Vote>
    {
        new void Add(Vote vote);
        int GetPoints(int answerId);
        int GetPoints(ApplicationUser user);
        void Remove(int answerId, string userId);
        IEnumerable<Vote> GetUserVotes(string userId, int questionId);
    }
}
