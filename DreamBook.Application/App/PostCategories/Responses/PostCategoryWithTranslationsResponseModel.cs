using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class PostCategoryWithTranslationsResponseModel : IResponseModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public Guid Guid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Word_Translations))]
        public List<PostCategoryTranslationResponseModel> Translations { get; set; }
    }
}
