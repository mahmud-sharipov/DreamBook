using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Users
{
    public class UpdateUserRequestModel : CreateUserRequestModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public Guid Guid { get; set; }
    }
}
