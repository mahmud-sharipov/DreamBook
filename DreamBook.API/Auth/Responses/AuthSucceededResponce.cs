using DreamBook.API.Auth.Model;

namespace DreamBook.API.Auth.Responses
{
    public class AuthSucceededResponce
    {
        public AuthSucceededResponce(JwtTokenResponse token, ApplicationUser user)
        {
            TokenInfo = token;
            User = user;
        }

        public JwtTokenResponse TokenInfo { get; }

        public ApplicationUser User { get; }
    }
}
