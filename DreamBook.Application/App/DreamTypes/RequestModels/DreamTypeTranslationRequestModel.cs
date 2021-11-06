using DreamBook.Application.Abstraction.Request;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.DreamTypes
{
    public class DreamTypeTranslationRequestModel : ITranslationRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.DreamTypeTranslation_Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.DreamTypeTranslation_Description))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.DreamTypeTranslation_Language))]
        public Guid LanguageGuid { get; set; }
    }
}