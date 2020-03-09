using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Domain;
using Translate.Models.Services;
using Translate.ViewModels.Builders;

namespace Translate.Controllers
{
    public class VoteController : BaseController
    {

        protected readonly IVoteService _voteService = new VoteService();

        public VoteController(IApplicationUser userService, IForum forumService, IViewModelBuilder builder) : base(userService, forumService, builder)
        {
        }

        class VoteResult
        {
            public int Points { get; set; }
        }

        public  JsonResult Vote(int value, int answer_id, string user_id )
        {
            if (value == 0)
            {
                //it means the client unclicked the button and the vote object must be deleted from db
                _uow.Votes.Remove(answer_id, user_id);
                _uow.SaveChanges();
                VoteResult res = new VoteResult()
                {
                    Points = _uow.Votes.GetPoints(answer_id)
                };

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
            VoteType voteType;
            if (value == 1)
                voteType = VoteType.Positive;
            else 
                voteType = VoteType.Negative;


           _uow.Votes.Add(new Vote()
            {
                VoteType = voteType,
                User = _uow.Users.Get(user_id),
                Answer = _uow.Answers.Get(answer_id)
            }) ;;
            _uow.SaveChanges();

            int points = _uow.Votes.GetPoints(answer_id);

            VoteResult result = new VoteResult()
            {
                Points = points
            };
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        // GET: Vote
        public ActionResult Index()
        {
            return View();
        }
    }
}