using DreamBook.API.Auth.Model;
using DreamBook.Application.Users;

namespace DreamBook.API.Auth.Responses
{
    public class AuthSucceededResponce
    {
        public AuthSucceededResponce(JwtTokenResponse token, UserResponseModel user)
        {
            TokenInfo = token;
            User = user;
        }

        public JwtTokenResponse TokenInfo { get; }

        public UserResponseModel User { get; }
    }
}
