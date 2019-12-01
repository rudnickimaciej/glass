using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models
{
    public class SpamReason
    {
        public SpamReason()
        {
        }

        public SpamReason(string content)
        {
            Content = content;
        }

        public int Id { get; set; }
        public string Content { get; set; }

    }
}