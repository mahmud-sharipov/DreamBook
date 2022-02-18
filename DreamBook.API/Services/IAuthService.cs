namespace DreamBook.API.Services;

public interface IAuthService
{
    Task<AuthSucceededResponse> Authenticate(LoginModel model);
    Task<AuthSucceededResponse> GoogleAuthentication(GoogleAuthRequest googleAuth);
    Task<bool> RevokeRefreshToken(string token);
    Task<JwtTokenResponse> RefreshToken(string token);
}
