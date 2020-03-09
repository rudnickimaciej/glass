using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
    public class UserRepository : Repository<ApplicationUser>, IUsersRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationUser Get(string id)
        {
            return Context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public ApplicationUser GetByName(string name)
        {
            return Context.Users.Where(u => u.UserName == name).FirstOrDefault();
        }

        public ICollection<ApplicationUser> GetUsers(int page, int pageSize)
        {
            return Context.Users.OrderByDescending(u => u.Rating).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        }
        
        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}