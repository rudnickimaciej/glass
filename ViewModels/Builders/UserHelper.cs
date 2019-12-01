using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.Models;

namespace Translate.ViewModels.Builders
{
    public class UserHelper
    {
        public static bool IsAuthorAdmin(ApplicationUser user)
        {
            if (user.Roles.Where(r => r.ToString().Equals("Admin")) != null)
            {
                return true;
            }
            return false;
        }
    }
}