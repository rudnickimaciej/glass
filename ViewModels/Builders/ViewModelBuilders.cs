using System.Linq;
using Translate.Models;
using Translate.ViewModels.Components;
using Microsoft.AspNet.Identity;
using System;
using Translate.ViewModels.Complex;

namespace Translate.ViewModels.Builders
{
    public  class ViewModelBuilder: IViewModelBuilder
    {

        public  QuestionViewModel BuildQuestion(Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                Content = question.Content,
                DatePosted = question.Created,
                AuthorId= question.User.Id,
                AuthorName = question.User.UserName,
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
                Points = answer.Points,
                QuestiondId = answer.Question.Id
                
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
                QuestionContent = question.Content

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
                QuestionContent = answer.Question.Content,
            }; 
        }

        public ProfileViewModel BuilProfileViewModel( ApplicationUser user)
        {
            return new ProfileViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Description = user.Description,
                IsActive = user.IsActive,
                MemberSince = user.MemberSince,
                ProfileImageUrl = user.ProfileImageUrl,
                UserPoints = user.Rating                 
            };
        }
    }
}