using DreamBook.API.Auth.Model;
using DreamBook.Application.Users;

namespace DreamBook.API.Auth.Responses
{
    public class AuthSucceededResponse
    {
        public AuthSucceededResponse(JwtTokenResponse token, UserResponseModel user)
        {
            TokenInfo = token;
            User = user;
        }

        public JwtTokenResponse TokenInfo { get; }

        public UserResponseModel User { get; }
    }
}
