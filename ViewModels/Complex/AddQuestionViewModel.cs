using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Translate.ViewModels.Complex
{
    public class AddQuestionViewModel
    {

        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}