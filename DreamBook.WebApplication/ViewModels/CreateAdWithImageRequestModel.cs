using DreamBook.Application.LanguageResources;
using DreamBook.Application.Ads;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.WebApplication.ViewModels
{
    public class CreateAdWithImageRequestModel : CreateAdRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Ad_Image))]
        public IFormFile ImageFile { get; set; }
    }
}
