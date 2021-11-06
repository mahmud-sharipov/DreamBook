using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Interpretations
{
    public class InterpretationResponseModel : IResponseModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public Guid Guid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public string Book { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public string Word { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.InterpretationTranslation_Description))]
        public string Description { get; set; }
    }
}