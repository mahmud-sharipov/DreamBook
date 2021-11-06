using DreamBook.Application.LanguageResources;
using DreamBook.Application.Languages;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryTranslationResponseModel : PostCategoryResponseModel
    {
        public LanguageResponseModel Language { get; set; }
    }
}
