using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Complex
{
    public class EditQuestionViewModel
    {
        public int Id { get; set; }
        public string LanguageFrom { get; set; }
        public string LanguageTo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}