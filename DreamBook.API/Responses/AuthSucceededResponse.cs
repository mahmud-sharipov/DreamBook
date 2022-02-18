namespace DreamBook.API.Responses
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
