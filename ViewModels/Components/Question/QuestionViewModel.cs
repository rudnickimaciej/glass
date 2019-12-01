using System;
namespace Translate.ViewModels.Components
{
    /// <summary>
    /// ViewModel for specific post inside particular language directory.
    /// </summary>
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public LanguageViewModel LanguageFrom { get; set; }
        public LanguageViewModel LanguageTo { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string AuthorImageUrl { get; set; }
        public int AuthorRating{ get; set; }

        public DateTime DatePosted { get; set; }
        public int AnswersCount { get; set; }



    }
}