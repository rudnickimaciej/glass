using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models.Domain
{
   public enum VoteType
    {
        Positive=1,
        Negative=-1,
        Clear =0
    }
    public class Vote
    {
        public int Id { get; set; }
        public virtual Answer Answer { get; set; }
        public VoteType VoteType { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}