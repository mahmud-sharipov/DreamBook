using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModel : ITranslatableRequestModel<PostCategoryTranslationRequestModel>
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Book_Translations))]
        public List<PostCategoryTranslationRequestModel> Translations { get; set; }
    }
}
