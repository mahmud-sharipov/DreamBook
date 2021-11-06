using DreamBook.Application.LanguageResources;
using DreamBook.Application.Languages;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryTranslationResponseModel : PostCategoryResponseModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Language))]
        public LanguageResponseModel Language { get; set; }
    }
}
