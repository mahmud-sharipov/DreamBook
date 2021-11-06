using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class CreateInterpretationRequestModel : ITranslatableRequestModel<InterpretationTranslationRequestModel>
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.InterpretationTranslation_Language))]
        public Guid BookGuid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.InterpretationTranslation_Language))]
        public Guid WordGuid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Interpretation_Translations))]
        public virtual List<InterpretationTranslationRequestModel> Translations { get; set; }
    }
}
