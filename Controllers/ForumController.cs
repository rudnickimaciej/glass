using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translate.Models;
using Translate.Models.Services;
using Translate.ViewModels.Components;
using Translate.ViewModels.Complex;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Translate.ViewModels.Builders;
using Microsoft.AspNetCore.Identity;
using Translate.ViewModels.Components.Pagination;
using Translate.Models.Domain;

namespace Translate.Controllers
{
    public class ForumController : BaseController
    {

        private const int QUESTIONS_PER_PAGE = 5;
        private const int USERS_PER_PAGE = 6;

        public ForumController(IApplicationUser userService, IForum forumService, IViewModelBuilder builder) : base(userService, forumService,builder)
        {
        }


        public ActionResult Questions(string searchQuery, int page =1)
        {
            int allQuestionsInDatabaseCount;
            int todayQuestionsCount;
            int allQuestionsMatchingQueryCount;
            List<Question> questions;
        
                allQuestionsInDatabaseCount = _uow.Questions.GetCount();
                todayQuestionsCount = 888;
                allQuestionsMatchingQueryCount = _uow.Questions.GetAllQuestionsMatchingQueryCount(searchQuery);
                questions = _uow.Questions.GetQuestionsWithPagination(searchQuery, page, QUESTIONS_PER_PAGE).ToList();

            

            // int todayQuestionsCount = _forumService.GetTodayQuestionsCunt();


            //var usersCount = _userService.GetAllUsersCount();
            var usersCount = _uow.Users.GetAll().Count();


            //If user passes too hight page number, then redirect to first page
            int pages = allQuestionsMatchingQueryCount / QUESTIONS_PER_PAGE;
            int remainingQuestions = allQuestionsMatchingQueryCount % QUESTIONS_PER_PAGE;

            if (remainingQuestions > 0)
                pages++;

            if (page > pages && allQuestionsInDatabaseCount!=0)
            {
                return RedirectToAction("Questions", "Forum",new { page = 1 });
            }
        

            var questionListing = questions.Select(q => _builder.BuildQuestion(q)).ToList();

            var model = new AllQuestionsOfForumViewModel
            {
                Questions = questionListing,
                QuestionsMatchingQueryCount = allQuestionsMatchingQueryCount,
                Statistics = new StatisticsViewModel
                {
                    QuestionsCount = allQuestionsInDatabaseCount,
                    ThisWeekQuestionsCount = todayQuestionsCount

                },
                Pagination = new PaginationModel()
                {
                    Count = allQuestionsMatchingQueryCount,
                    CurrentPage = page,
                    PageSize = QUESTIONS_PER_PAGE,
                    Url = "questions/"
                }, 
                TopAnswers = _uow.Answers.GetTop(5).Select(a => _builder.BuildAnswer(a))
            };
            return View("~/Views/Home/Index.cshtml", model);
        }

        /// <summary>
        /// Returns one specific Question.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public ActionResult Question(int questionId)
        {
            var question = _uow.Questions.Get(questionId);
            var userVotes = _uow.Votes.GetUserVotes(User.Identity.GetUserId(), question.Id);

            Dictionary<int, VoteType> voteDict = new Dictionary<int, VoteType>();
            userVotes.ToList().ForEach(v => voteDict.Add(v.Answer.Id, v.VoteType.Equals(VoteType.Positive) ? VoteType.Positive : VoteType.Negative));

            QuestionPageViewModel viewModel = new QuestionPageViewModel
            {
                Question = _builder.BuildQuestion(question),
                Answers = question.Answers.OrderByDescending(a=>a.Points).Select(a => _builder.BuildAnswer(a)),
                IsAdmin = UserHelper.IsAuthorAdmin(question.User),
                ActiveUserId = User.Identity.GetUserId(),
                VotesDictionary = voteDict
            };

            if (viewModel.ActiveUserId == null)
                viewModel.ActiveUserId = string.Empty;
            return View("~/Views/Question/Question.cshtml", viewModel);
        }


        


        //GET: questions/add
        [Authorize]
        public ActionResult AddQuestion()
        {

            //TODO: Po wciśnięciu przycisku Dodaj pytanie będąc w danym forum, w widoku dodawania pytania
            // języki powinny być już ustawione
            var model = new AddQuestionViewModel
            {
                AuthorName = User.Identity.Name
            };


            return View("~/Views/Question/AddQuestion.cshtml", model);
        }


        /// <summary>
        /// Asynchronous method to add question to Database and redirect to newly created Question.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddQuestion(AddQuestionViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var question = new Question
            {
                Id = -1,
                User = _uow.Users.Get(userId),
                Content = model.Content,
                Created = DateTime.Now
            };

            
             var addedQuestion = _uow.Questions.Add(question);
            _uow.SaveChanges();
            
            return RedirectToAction("Question",new { questionId = question.Id });
        }

        public ActionResult EditQuestion(string langFrom, string langTo, int questionId)
        {
           var question = _forumService.GetQuestion(questionId);
           var model =  new EditQuestionViewModel
            { 
                Id = question.Id,
                Content = question.Content     
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditQuestion(EditQuestionViewModel editedQuestion)
        {
            var question = _forumService.EditQuestion(new EditQuestionFields
            {
                Id = editedQuestion.Id,
                Content = editedQuestion.Content,
                Title = editedQuestion.Title

            });

            return RedirectToAction("Question", new { questionId = editedQuestion.Id });
        }

        //POST: forum/deletequestion
        public ActionResult DeleteQuestion(int questionId)
        {
            _forumService.DeleteQuestion(questionId);
            return RedirectToAction("Questions", "Forum", null);
        }

        [Authorize]
        public ActionResult CreateAnswer(int questionId)
        {
            
            var userId= User.Identity.GetUserId();
            var user=_uow.Users.Get(userId);

            var question =_uow.Questions.Get(questionId);
            var model = _builder.BuildReplyViewModel(question, user);

            return View("~/Views/Question/AddAnswer.cshtml", model);
        }

        public ActionResult AddAnswer(ReplyViewModel model)
        {
            var question = _uow.Questions.Get(model.QuestionId);


            var answer = new Answer()
            {
                Content = model.ReplyContent,
                Created = model.Created,
                Question = question           
                //User = _userService.GetById(model.AuthorId)
            };
            answer.User = _uow.Users.Get(model.AuthorId);
            _uow.Answers.Add(answer);
            _uow.SaveChanges();
            return RedirectToAction("Question", new {questionId = model.QuestionId });
        }

        //GET:forum/editanswer/{answerId}
        public ActionResult EditAnswer(int answerId)
        {
            var answer = _forumService.GetAnswerById(answerId);

            var user = _userService.GetById(answer.User.Id);

            var question = answer.Question;

            var model = _builder.BuilEditAnswerViewModel(answer, user);
            //model.ReplyContent = answer.Content; 

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel model)
        {
            _forumService.EditAnswer(model.AnswerId, model.ReplyContent);
            return RedirectToAction("Question", new { questionId = model.QuestionId });
        }

        public ActionResult DeleteAnswer(int answerId)
        {
            var question = _forumService.GetAnswerById(answerId).Question;
            _forumService.DeleteAnswer(answerId);
            return RedirectToAction("Question", new { questionId = question.Id });
        }


        public ActionResult Ranking(int page = 1)
        {
            var allUsers = _uow.Users.GetAll().ToList();
            allUsers.ForEach(u => u.Rating = _uow.Votes.GetPoints(u));
            _uow.SaveChanges();

            var newQuestions = _uow.Questions.GetNewQuestions(5);
            var users = _uow.Users.GetUsers(page, USERS_PER_PAGE).ToList(); 
        
            users.ForEach(u => u.Rating = _uow.Votes.GetPoints(u));
            users.OrderBy(u => u.Rating);
            int allUsersCount = _uow.Users.GetCount();

            int pages = allUsersCount / USERS_PER_PAGE;
            int remainingUsers = allUsersCount % USERS_PER_PAGE;

            if (remainingUsers > 0)
                pages++;

            if (page > pages)
            {
                return RedirectToAction("Ranking", "Forum", new { page = 1 });
            }


            RankingViewModel model = new RankingViewModel
            {
                Users = users.Select(user => new ProfileViewModel()
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Description = user.Description,
                    Email = user.Email,
                    MemberSince = user.MemberSince,
                    ProfileImageUrl = user.ProfileImageUrl,
                    UserPoints = user.Rating,
                    IsActive = user.IsActive,
                    UserQuestions = _uow.Questions.Find(q => q.User.Id == user.Id).Select(q => _builder.BuildQuestion(q)),
                    UserAnswers = _uow.Answers.Find(a => a.User.Id == user.Id).Select(a => _builder.BuildAnswer(a))

                }),
                NewQuestions = newQuestions.Select(q => _builder.BuildQuestion(q)),

                Pagination = new PaginationModel()
                {
                    Count= allUsersCount,
                    CurrentPage= page,
                    PageSize= USERS_PER_PAGE,
                    Url="ranking/"
                }

            };

            return View("~/Views/Ranking/Ranking.cshtml", model);
        }

        #region helper Methods

      
        #endregion
    }

}