using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Translate.Models.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
    


        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id == id);
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return GetAll().FirstOrDefault(user => user.UserName == userName);
        }

        IEnumerable<ApplicationUser> IApplicationUser.GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }
        public Task IncrementRating(string id, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            await _context.SaveChangesAsync();

        }
    }
}