using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models
{
    public abstract class Reportable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}