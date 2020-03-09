using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
   public interface IUsersRepository: IRepository<ApplicationUser>
    {
        ApplicationUser Get(string id);
        ApplicationUser GetByName(string name);

        ICollection<ApplicationUser> GetUsers(int page, int pageSize);

    }
}
