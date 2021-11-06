using DreamBook.Application.Users;

namespace DreamBook.API.Auth.Requests
{
    public class UserRegisterModel : CreateUserRequestModel
    {
        public string Password { get; set; }
    }
}
