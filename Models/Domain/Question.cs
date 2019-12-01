using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels;

namespace Translate.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual Language LanguageFrom { get; set; }
        public virtual Language LanguageTo { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }
}