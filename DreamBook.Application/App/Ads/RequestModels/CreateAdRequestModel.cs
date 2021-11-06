using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Ads
{
    public class CreateAdRequestModel : ITranslatableRequestModel<AdTranslationRequestModel>
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Ad_Image))]
        public string Image { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Ad_Source))]
        public string Source { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Ad_Translations))]
        public List<AdTranslationRequestModel> Translations { get; set; }
    }
}