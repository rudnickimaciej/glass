using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Translate.Models;
using Translate.Models.Services;
using Translate.ViewModels.Components;

namespace Translate.Controllers
{
    public class ProfileController : BaseController
    {

        private readonly IUpload _uploadService = new UploadService();


        //[Authorize(Roles="User")]
        // GET: Profile
        public ActionResult Details(string userName)
        {
            var user = _userService.GetByUserName(userName);

            //var userRoles = _userManager.GetRolesAsync(userId).Result;
            var model = new ProfileViewModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                Description =user.Description,
                Email = user.Email,
                MemberSince = user.MemberSince,
                ProfileImageUrl = user.ProfileImageUrl,
                UserRating = user.Rating,
                IsActive = user.IsActive,
                //IsAdmin = userRoles.Contains("Admin")
              
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(IFormFile file)
        {
            //TODO
            throw new Exception();
        }
    }
}