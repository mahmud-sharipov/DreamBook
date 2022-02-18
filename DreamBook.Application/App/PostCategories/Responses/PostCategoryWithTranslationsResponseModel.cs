using System.Collections.Generic;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryWithTranslationsResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }

        public List<PostCategoryTranslationResponseModel> Translations { get; set; }
    }
}
