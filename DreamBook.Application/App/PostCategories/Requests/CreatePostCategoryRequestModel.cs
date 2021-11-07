using DreamBook.Application.Abstraction.Request;
using System.Collections.Generic;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModel : ITranslatableRequestModel<PostCategoryTranslationRequestModel>
    {
        public List<PostCategoryTranslationRequestModel> Translations { get; set; }
    }
}
