using DreamBook.Application.LanguageResources;
using DreamBook.Application.Posts;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.WebApplication.ViewModels
{
    public class UpdatePostWithImageRequestModel : UpdatePostRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Image))]
        public IFormFile ImageFile { get; set; }
    }
}
