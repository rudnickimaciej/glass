﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.Models.Domain;

namespace Translate.Models.Services
{
    public class VoteService:IVoteService
    {
        private ApplicationDbContext _context;

        public VoteService()
        {
            _context = new ApplicationDbContext();
        }

        public int Vote(Vote vote)
        {
            var voteToRemove = _context.Votes.
                 Where(v => v.Answer.Id == vote.Answer.Id).
                 Where(v => v.User.Id == vote.User.Id).FirstOrDefault();

            if (voteToRemove != null)
                _context.Votes.Remove(voteToRemove);

           _context.Votes.Add(vote);
           _context.SaveChanges();

            return _context.Answers.Where(a => a.Id == vote.Answer.Id).FirstOrDefault().Points;
        }
    }
}