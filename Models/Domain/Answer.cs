using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Translate.Models.Domain;
using Translate.ViewModels;

namespace Translate.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        [NotMapped]
        public int Points
        {
            get
            {
                int val = 0;
                foreach (Vote v in Votes)
                {
                    if (v.VoteType.Equals(VoteType.Positive))
                        val++;
                    else if (v.VoteType.Equals(VoteType.Negative))
                        val--;
                }
                return val;
            }
        }

    }
}