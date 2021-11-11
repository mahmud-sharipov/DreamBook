using DreamBook.Application.Abstraction.Response;
using DreamBook.Domain.Enums;
using System;

namespace DreamBook.Application.Users
{
    public class UserResponseModel : IResponseModel
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarImage { get; set; }
    }
}
