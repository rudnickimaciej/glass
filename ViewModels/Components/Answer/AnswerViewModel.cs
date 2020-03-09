using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.Models;

namespace Translate.ViewModels.Components
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string ShortContent(int length)
        {

            if (Content.Length < length)
                return Content;
            else
                return Content.Substring(0, length + 1);
        }
        public DateTime Created { get; set; }

        public string DateFormat
        {
            get { return Created.ToString("MM/dd/yyyy HH:mm"); }
            private set { }
        }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl{ get; set; }
        public bool AuthorIsAdmin { get; set; }

        public int AuthorRating { get; set; }
        public int Points { get; set; }

        public int QuestiondId { get; set; }


    }
}