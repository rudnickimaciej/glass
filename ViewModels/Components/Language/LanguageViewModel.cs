﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Components
{
    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string ImageUrl { get; set; }
    }
}