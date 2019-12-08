using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.ViewModels.Components
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public int UserPoints { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }


        public DateTime? MemberSince { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}