using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Posts
{
    public class CreatePostRequestModel : IRequestModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public Guid CategoryGuid { get; set; }
    }
}
