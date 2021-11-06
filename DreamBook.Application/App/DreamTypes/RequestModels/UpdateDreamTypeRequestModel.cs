using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.DreamTypes
{
    public class UpdateDreamTypeRequestModel : CreateDreamTypeRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.EntityGuid))]
        public Guid Guid { get; set; }
    }
}
