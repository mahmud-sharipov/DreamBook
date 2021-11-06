using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationTranslationRequestModel : ITranslationRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.InterpretationTranslation_Description))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.InterpretationTranslation_Language))]
        public Guid LanguageGuid { get; set; }
    }
}
