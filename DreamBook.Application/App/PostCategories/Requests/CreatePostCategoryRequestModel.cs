using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModel : ITranslatableRequestModel<PostCategoryTranslationRequestModel>
    {
        public List<PostCategoryTranslationRequestModel> Translations { get; set; }
    }
}
