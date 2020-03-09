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
using Translate.ViewModels.Builders;
using Translate.ViewModels.Components;
using static Translate.Controllers.ManageController;

namespace Translate.Controllers
{
    public class ProfileController : BaseController
    {

        private readonly IUpload _uploadService;
        protected readonly IVoteService _voteService;

        public ProfileController(IApplicationUser userService, IForum forumService, IUpload uploadService,IVoteService voteService, IViewModelBuilder builder) : base(userService, forumService, builder)
        {
            _uploadService = uploadService;
            _voteService = voteService;
        }

        //[Authorize(Roles="User")]
        // GET: profiles/{userName}
        public ActionResult UserProfile(string userName, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione.":"";

            var user = _uow.Users.GetByName(userName);
            user.Rating = _uow.Votes.GetPoints(user);

            //var userRoles = _userManager.GetRolesAsync(userId).Result;
            var model = new ProfileViewModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                Description = user.Description,
                Email = user.Email,
                MemberSince = user.MemberSince,
                ProfileImageUrl = user.ProfileImageUrl,
                UserPoints = user.Rating,
                IsActive = user.IsActive,
                UserQuestions = _uow.Questions.Find(q=>q.User.Id==user.Id).Select(q=>_builder.BuildQuestion(q)),
                UserAnswers = _uow.Answers.Find(a=>a.User.Id==user.Id).Select(a=>_builder.BuildAnswer(a))

                //IsAdmin = userRoles.Contains("Admin")

            };
            return View("~/Views/Profile/Details.cshtml",model);
        }

        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(IFormFile file)
        {
            //TODO
            throw new Exception();
        }
    }
}