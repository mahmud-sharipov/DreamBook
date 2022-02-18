using System.Collections.Generic;

namespace DreamBook.Application.PostCategories
{
    public class CreatePostCategoryRequestModel : ITranslatableRequestModel<PostCategoryTranslationRequestModel>
    {
        public List<PostCategoryTranslationRequestModel> Translations { get; set; }
    }
}
