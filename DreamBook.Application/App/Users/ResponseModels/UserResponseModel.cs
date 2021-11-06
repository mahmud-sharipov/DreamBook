using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Users
{
    public class UserResponseModel : IResponseModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public Guid Guid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.User_Username))]
        public string Username { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.User_Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.User_Name))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.User_Gender))]
        public Gender Gender { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.User_Birthday))]
        public DateTime Birthday { get; set; }
    }
}
