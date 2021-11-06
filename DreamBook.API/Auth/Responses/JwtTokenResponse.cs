using System;

namespace DreamBook.API.Auth.Responses
{
    public class JwtTokenResponse
    {
        public JwtTokenResponse() { }

        public JwtTokenResponse(string accessToken, DateTime dateTime)
        {
            AccessToken = accessToken;
            AccessTokenValidTo = dateTime;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime AccessTokenValidTo { get; set; }
        public DateTime RefreshTokenValidTo { get; set; }
    }
}
