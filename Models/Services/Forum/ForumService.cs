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

      
        public Question GetQuestion(int questionId)

        {
            return _context.Questions.Where(q => q.Id == questionId).
                Include(q => q.Answers.Select(a => a.User)).
                Include(q => q.User).FirstOrDefault();
        }

        public int GetAllQuestionsCount()
        {
            return _context.Questions.Count();
        }

        public  int GetAllQuestionsMatchingQueryCount(string query)
        {
            if (query != null)
                return _context.Questions.Where(q => q.Content.Contains(query)).Count();
            else return GetAllQuestionsCount();
        }

        public IEnumerable<Question> GetQuestions(string query,int currentPage,int pageSize)
        {
            if(query!=null)
                return _context.Questions.Where(q=>q.Content.Contains(query)).OrderBy(q=>q.Created).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            else
                return _context.Questions.OrderBy(q => q.Created).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        }
        public IEnumerable<Question> GetQuestions(string query)
        {
            IQueryable<Question> questions;
            if (string.IsNullOrEmpty(query))
                questions = _context.Questions;

            else
                questions = _context.Questions.Where(q => q.Content.Contains(query));
       
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
public        IEnumerable<Question> GetUserQuestions(string userId)
        {
            return _context.Questions.Where(q => q.User.Id == userId).ToList();
        }
      public  IEnumerable<Answer> GetUserAnswers(string userId)
        {
            return _context.Answers.Where(a => a.User.Id == userId).ToList();
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

            questionToEdit.Content = editQuestion.Content;
            _context.SaveChanges();

            //_context.Entry(questionToEdit).Reload();
            return questionToEdit;
        }

        public void DeleteQuestion(int questionId)
        {
            IList<Answer> answers =_context.Answers.Where(a => a.Question.Id == questionId).ToList();
            foreach (Answer a in answers)
                DeleteAnswer(a.Id);

            var questionToDelete = _context.Questions.Where(q => q.Id == questionId).FirstOrDefault();
            _context.Questions.Remove(questionToDelete);
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


        public void EditAnswer(int answerId, string newContent)
        {
            _context.Answers.Where(a => a.Id == answerId).FirstOrDefault().
                Content = newContent;
            _context.SaveChanges();
        }

        public void DeleteAnswer(int answerId)
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
                Include(q => q.User);
               
        }




        #endregion

    }
}