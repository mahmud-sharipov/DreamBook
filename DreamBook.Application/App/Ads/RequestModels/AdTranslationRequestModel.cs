using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Ads
{
    public class AdTranslationRequestModel : ITranslationRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.AdTranslation_Title))]
        public string Title { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.AdTranslation_Description))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.AdTranslation_Language))]
        public Guid LanguageGuid { get; set; }
    }
}
