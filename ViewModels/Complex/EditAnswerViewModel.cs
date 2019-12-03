﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Translate.ViewModels.Components;

namespace Translate.ViewModels.Complex
{
    public class EditAnswerViewModel
    {
        public int AnswerId { get; set; }
        public string ReplyContent { get; set; }

        public DateTime Created { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }

        public int LanguageFromId { get; set; }
        public string LanguageFromImageUrl{ get; set; }
        public string LanguageFromAbbreviation { get; set; }

        public int LanguageToId { get; set; }
        public string LanguageToImageUrl { get; set; }
        public string LanguageToAbbreviation { get; set; }

    }
}