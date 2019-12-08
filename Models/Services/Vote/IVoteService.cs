using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Models.Domain;

namespace Translate.Models.Services
{
    public interface IVoteService
    {
        int Vote(Vote vote);
        int GetUserPoints(string userId);

    }
}
