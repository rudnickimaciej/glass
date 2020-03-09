using System;
using Translate.ViewModels.Helpers;
namespace Translate.ViewModels.Components
{
    /// <summary>
    /// ViewModel for specific post inside particular language directory.
    /// </summary>
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        
        public string ShortContent(int length) {

            if (Content.Length < length)
                return Content;
            else
                return Content.Substring(0, length +1);
        }


        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string AuthorImageUrl { get; set; }
        public int AuthorRating{ get; set; }

        public DateTime DatePosted { get; set; }
        public string DateFormat
        {
            get { return DatePosted.ToString("MM/dd/yyyy HH:mm"); }
            private set { }
        }
        public int AnswersCount { get; set; }



    }
}