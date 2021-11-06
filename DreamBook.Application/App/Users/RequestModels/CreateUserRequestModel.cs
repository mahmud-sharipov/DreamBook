using DreamBook.Application.LanguageResources;
using DreamBook.Application.Abstraction.Request;
using DreamBook.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Users
{
    public class CreateUserRequestModel : IRequestModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }
    }
}
