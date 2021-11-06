using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.PostCategories
{
    public class UpdatePostCategoryRequestModel : CreatePostCategoryRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.EntityGuid))]
        public Guid Guid { get; set; }
    }
}
