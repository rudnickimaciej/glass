using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Components
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string ReplyContent { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime Created { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }

    }
}