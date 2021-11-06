using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Ads
{
    public class UpdateAdRequestModel : CreateAdRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.EntityGuid))]
        public Guid Guid { get; set; }
    }
}