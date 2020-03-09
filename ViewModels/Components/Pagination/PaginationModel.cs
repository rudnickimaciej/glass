using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Components.Pagination
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }

        public string Url { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
    }
}