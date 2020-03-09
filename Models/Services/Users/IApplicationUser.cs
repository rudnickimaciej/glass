using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate.Models.Services
{
  public interface IApplicationUser  
   {
        ApplicationUser GetById(string id);
        ApplicationUser GetByUserName(string userName);
        IEnumerable<ApplicationUser> GetAllActiveUsers();
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> GetUsers(int page, int pageSize);
        Task SetProfileImage(string id, Uri uri);
        Task IncrementRating(string id, Type type);
    }
}
