using System.Linq;
using Translate.Models;
using Translate.ViewModels.Components;
using Microsoft.AspNet.Identity;
using System;
using Translate.ViewModels.Complex;

namespace Translate.ViewModels.Builders
{
    public  class ViewModelBuilder
    {

        public  QuestionViewModel BuildQuestion(Question question)
        {
            
            return new QuestionViewModel
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                DatePosted = question.Created,
                LanguageFrom = BuildLanguage(question.LanguageFrom),
                LanguageTo=BuildLanguage(question.LanguageTo),
                AuthorId= "0c14d9f8-76d5-4b97-a73e-e0960e58540b",
                AuthorName = "UserName",
                AuthorImageUrl = "/Content/images/users/user_0",
                AnswersCount = question.Answers.Count()
            };
        }

        public  AnswerViewModel BuildAnswer(Answer answer)
        {
            return new AnswerViewModel
            {
                Id = answer.Id,
                Content = answer.Content,
                Created = answer.Created,
                AuthorIsAdmin = UserHelper.IsAuthorAdmin(answer.User),
                AuthorId=answer.User.Id,
                AuthorName=answer.User.UserName,
                AuthorImageUrl = answer.User.ProfileImageUrl,
                Points = answer.Points
                
            };
        }

        public  LanguageViewModel BuildLanguage(Language language)
        {
            return new LanguageViewModel
            {
                Id = language.Id,
                Name = language.Name,
                Abbreviation = language.Abbreviation,
                ImageUrl = language.ImageUrl
            };
        }
        
        public ReplyViewModel BuildReplyViewModel(Question question, ApplicationUser user)
        {
            return  new ReplyViewModel()
            {
                AuthorId = user.Id,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorName = user.UserName,
                AuthorRating = user.Rating,
                Created = DateTime.Now,
                QuestionId = question.Id,
                QuestionTitle = question.Title,
                QuestionContent = question.Content,
                LanguageFrom = BuildLanguage(question.LanguageFrom),
                LanguageTo = BuildLanguage(question.LanguageTo)
            };
        }
        public EditAnswerViewModel BuilEditAnswerViewModel(Answer answer ,ApplicationUser user)
        {
            return new EditAnswerViewModel()
            {
                QuestionId = answer.Question.Id,
                AnswerId = answer.Id,
                ReplyContent = answer.Content,
                Created = answer.Created,
                LanguageFrom = BuildLanguage(answer.Question.LanguageFrom),
                LanguageTo = BuildLanguage(answer.Question.LanguageTo),
                QuestionContent = answer.Question.Content,
                QuestionTitle = answer.Question.Title
            }; 
        }

    }
}