using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Users
{
    public class UserResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
