using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Translate.Models.Domain;
using Translate.Repositories;

namespace Translate.Models.Repositiories
{
    public  class VoteRepository:Repository<Vote>,IVoteRepository
    {
        public VoteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public void Add(Vote vote)
        {
            var voteToRemove = Context.Votes.
              Where(v => v.Answer.Id == vote.Answer.Id).
              Where(v => v.User.Id == vote.User.Id).FirstOrDefault();

            if (voteToRemove != null)
                Context.Votes.Remove(voteToRemove);

            Context.Votes.Add(vote);
        }
        public int GetPoints(int answerId)
        {
            return Context.Answers.Where(a => a.Id == answerId).FirstOrDefault().Points;

        }

        public int GetPoints(ApplicationUser user)
        {
            var votes = Context.Votes.Where(v => v.Answer.User.Id == user.Id);

            int sum = 0;
            foreach (Vote v in votes)
            {
                if (v.VoteType.Equals(VoteType.Positive))
                    sum++;
                else if (v.VoteType.Equals(VoteType.Negative))
                    sum--;
            }
            return sum;
        }

        public void Remove(int answerId, string userId)
        {
            var voteToRemove = Context.Votes.Where(v => v.Answer.Id.Equals(answerId) && v.User.Id.Equals(userId)).FirstOrDefault();
            Context.Votes.Remove(voteToRemove);
        }


        public IEnumerable<Vote> GetUserVotes(string userId, int questionId)
        {
            return Context.Votes.Where(v => v.User.Id.Equals(userId) && v.Answer.Question.Id.Equals(questionId));
        }


        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}