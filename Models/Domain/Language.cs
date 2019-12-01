﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}