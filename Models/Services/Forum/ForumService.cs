using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Translate.Models.Services
{
    public class ForumService : IForum
    {
        private ApplicationDbContext _context;

        public ForumService()
        {
            _context = new ApplicationDbContext();
        }

        public Language GetLanguage(string abbr)
        {
            return _context.Languages.Where(l => l.Abbreviation == abbr).FirstOrDefault();
        }
        public IEnumerable<Language> GetAllLanguages()
        {
            return _context.Languages;
        }
        public Question GetQuestion(int questionId)

        {
            return _context.Questions.Where(q => q.Id == questionId).
                Include(q => q.Answers.Select(a => a.User)).
                Include(q => q.User).
                Include(q => q.LanguageFrom).
                Include(q => q.LanguageTo).FirstOrDefault();
        }
        public IEnumerable<Question> GetQuestions(string langFrom, string langTo)
        {
            IQueryable<Question> questions;
            if (string.IsNullOrEmpty(langFrom) && string.IsNullOrEmpty(langTo))
                questions = _context.Questions;

            else if (!string.IsNullOrEmpty(langFrom) && string.IsNullOrEmpty(langTo))
                questions = _context.Questions.Where(q => q.LanguageFrom.Abbreviation == langFrom);

            else if (string.IsNullOrEmpty(langFrom) && !string.IsNullOrEmpty(langTo))
                questions = _context.Questions.Where(q => q.LanguageTo.Abbreviation == langTo);

            else
                questions = _context.Questions.Where(f => (f.LanguageFrom.Abbreviation == langFrom) && (f.LanguageTo.Abbreviation == langTo));

            return QuestionInclude(questions);
        }

        public IEnumerable<Question> GetLatestQuestions(int amount)
        {
            var latest = _context.Questions.OrderByDescending(q => q.Created).Take(amount);
            return QuestionInclude(latest);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            var questions = _context.Questions;
            return QuestionInclude(questions);

        }

        public Question AddQuestion(Question question)
        {
           var q=_context.Questions.Add(question);
            _context.SaveChanges();
            return q;
        }


        public Question EditQuestion(EditQuestionFields editQuestion)
        {
            var questionToEdit = _context.Questions.SingleOrDefault(q => q.Id == editQuestion.Id);
            questionToEdit.LanguageFrom = GetLanguage(editQuestion.LanguageFrom);
            questionToEdit.LanguageTo = GetLanguage(editQuestion.LanguageTo);
            questionToEdit.Content = editQuestion.Content;
            questionToEdit.Title = editQuestion.Title;
            _context.SaveChanges();

            _context.Entry(questionToEdit).Reload();
            return questionToEdit;
        }

        public void DeleteQuestion(int questionId)
        {
            var answers =_context.Answers.Where(a => a.Question.Id == questionId);
            _context.Answers.RemoveRange(answers);
            _context.Questions.Remove(_context.Questions.Where(q=>q.Id==questionId).FirstOrDefault());
            _context.SaveChanges();
        }

        //Answers
        public Answer GetAnswerById(int answerId)
        {
           return _context.Answers.Where(a => a.Id == answerId).FirstOrDefault();
        }

        public void AddAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }


        void IForum.EditAnswer(int answerId, string newContent)
        {
            _context.Answers.Where(a => a.Id == answerId).FirstOrDefault().
                Content = newContent;
            _context.SaveChanges();
        }

        void IForum.DeleteAnswer(int answerId)
        {
            var reports = _context.SpamReports.Where(s => s.ReportedAnswer.Id == answerId);
            _context.SpamReports.RemoveRange(reports);
            var votes = _context.Votes.Where(v => v.Answer.Id == answerId);
            _context.Votes.RemoveRange(votes);
            var answer = _context.Answers.Where(a => a.Id == answerId).FirstOrDefault();
            _context.Answers.Remove(answer);

            _context.SaveChanges();

        }


        #region helper methods


        /// <summary>
        /// Includes Answers for each Question object in IEnumerable.
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>


        private IEnumerable<Question> QuestionInclude(IQueryable<Question> questions)
        {
            return questions.
                Include(q => q.Answers).
                Include(q=>q.User).
                Include(q=>q.LanguageFrom).
                Include(q=>q.LanguageTo);
               
        }

 
        #endregion

    }
}