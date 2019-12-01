using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Translate.Models.Domain;
using Translate.Models.Services;

namespace Translate.Controllers
{
    public class VoteController : BaseController
    {

        protected readonly IVoteService _voteService = new VoteService();

        class VoteResult
        {
            public int Points { get; set; }
        }

        public  JsonResult Vote(int value, int answer_id, string user_id )
        {
            VoteType voteType;
            if (value == 1)
                voteType = VoteType.Positive;
            else 
                voteType = VoteType.Negative;
            
            int points=_voteService.Vote(new Vote()
            {
                VoteType = voteType,
                User = _userService.GetById(user_id),
                Answer = _forumService.GetAnswerById(answer_id)
            });

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