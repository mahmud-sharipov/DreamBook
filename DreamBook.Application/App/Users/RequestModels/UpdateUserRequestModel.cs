using System;

namespace DreamBook.Application.Users
{
    public class UpdateUserRequestModel : CreateUserRequestModel
    {
        public Guid Guid { get; set; }
    }
}
