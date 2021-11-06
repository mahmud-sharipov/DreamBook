using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.DreamTypes
{
    public class CreateDreamTypeRequestModel : ITranslatableRequestModel<DreamTypeTranslationRequestModel>
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.DreamType_Color))]
        public string Color { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.DreamType_Translations))]
        public List<DreamTypeTranslationRequestModel> Translations { get; set; }
    }
}
