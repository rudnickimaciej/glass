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


namespace Translate.Controllers
{
    public class ForumController : BaseController
    {


        /// <summary>
        /// Returns all questions associated with Forum with langFrom and langTo
        /// </summary>
        /// <returns></returns>
        /// 

       public ActionResult Index()
       {
            var latest = _forumService.GetLatestQuestions(10).ToList();
            var questionListing = latest.Select(q => _builder.BuildQuestion(q)).ToList();
            var model = new AllQuestionsOfForumViewModel
            {
                Questions = questionListing

            };
            return View("Questions", model);
        }
        public ActionResult Questions(string langFrom="", string langTo="")
        {
            var questions = _forumService.GetQuestions(langFrom, langTo).ToList();

            var questionListing = questions.Select(q => _builder.BuildQuestion(q)).ToList();

            var model = new AllQuestionsOfForumViewModel
            {
                Questions = questionListing,
                
               
            };
            return View("Questions",model);
        }
    
        /// <summary>
        /// Returns one specific Question.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public ActionResult Question(string langFrom, string langTo, int questionId)
        {
            var question = _forumService.GetQuestion(questionId);

            QuestionPageViewModel viewModel = new QuestionPageViewModel
            {
                Question = _builder.BuildQuestion(question),
                Answers = question.Answers.OrderByDescending(a=>a.Points).Select(a => _builder.BuildAnswer(a)),
                IsAdmin = UserHelper.IsAuthorAdmin(question.User),
                ActiveUserId = User.Identity.GetUserId()
             };

            if (viewModel.ActiveUserId == null)
                viewModel.ActiveUserId = string.Empty;
            return View("Question",viewModel);
        }
        
        /// <summary>
        /// Returns AddQuestion Page with forms to fill and button to submit new question.
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        /// 

        public ActionResult AddQuestion()
        {

            //TODO: Po wciśnięciu przycisku Dodaj pytanie będąc w danym forum, w widoku dodawania pytania
            // języki powinny być już ustawione
            var model = new AddQuestionViewModel
            {
                AuthorName = User.Identity.Name
            };


            return View("AddQuestion", model);
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
                User=_userService.GetById(userId),
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                LanguageFrom=_forumService.GetLanguage(model.LanguageFrom),
                LanguageTo = _forumService.GetLanguage(model.LanguageTo)
            };

           var addedQuestion=_forumService.AddQuestion(question);
            return RedirectToAction("Question", new { langFrom = model.LanguageFrom, langTo = model.LanguageTo,questionId = addedQuestion.Id});
        }

        public ActionResult EditQuestion(string langFrom, string langTo, int questionId)
        {
           var question = _forumService.GetQuestion(questionId);
           var model =  new EditQuestionViewModel
            { 
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                LanguageFrom = question.LanguageFrom.Abbreviation,
                LanguageTo = question.LanguageTo.Abbreviation          
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
                Title = editedQuestion.Title,
                LanguageFrom = editedQuestion.LanguageFrom,
                LanguageTo = editedQuestion.LanguageTo
            });

            return RedirectToAction("Question", new { langFrom = editedQuestion.LanguageFrom, langTo = editedQuestion.LanguageTo, questionId = editedQuestion.Id });
        }

        //POST: forum/deletequestion
        public ActionResult DeleteQuestion(int questionId)
        {
            _forumService.DeleteQuestion(questionId);
            return RedirectToAction("Index", "Home", null);
        }

        [Authorize]
        public ActionResult CreateAnswer(string langFrom, string langTo,int questionId)
        {
            
            var userId= User.Identity.GetUserId();
            var user=_userService.GetById(userId);

            var question = _forumService.GetQuestion(questionId);
            var model = _builder.BuildReplyViewModel(question, user);

            return View("AddAnswer",model);
        }

        public ActionResult AddAnswer(ReplyViewModel model)
        {
            var question = _forumService.GetQuestion(model.QuestionId);
            var langFrom = question.LanguageFrom;
            var langTo = question.LanguageTo;

            var answer = new Answer()
            {
                Content = model.ReplyContent,
                Created = model.Created,
                Question = question,           
                User = _userService.GetById(model.AuthorId)
            };
            _forumService.AddAnswer(answer);
            return RedirectToAction("Question", new { langFrom = langFrom.Abbreviation, langTo = langTo.Abbreviation, questionId = model.QuestionId });
        }

        //GET:forum/editanswer/{answerId}
        public ActionResult EditAnswer(int answerId)
        {
            var answer = _forumService.GetAnswerById(answerId);

            var user = _userService.GetById(answer.User.Id);

            var question = answer.Question;

            var model = _builder.BuilEditAnswerViewModel(answer, user);
            model.ReplyContent = answer.Content; 

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel model)
        {
            _forumService.EditAnswer(model.AnswerId, model.ReplyContent);
            return RedirectToAction("Question", new { langFrom = model.LanguageFrom.Abbreviation, langTo = model.LanguageTo.Abbreviation, questionId = model.QuestionId });
        }

        public ActionResult DeleteAnswer(int answerId)
        {
            var question = _forumService.GetAnswerById(answerId).Question;
            _forumService.DeleteAnswer(answerId);
            return RedirectToAction("Question", new { langFrom = question.LanguageFrom.Abbreviation, langTo = question.LanguageTo.Abbreviation, questionId = question.Id });
        }




        #region helper Methods

      
        #endregion
    }

}