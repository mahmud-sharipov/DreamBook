using DreamBook.API.Auth.Model;
using DreamBook.API.Auth.Requests;
using DreamBook.API.Auth.Responses;
using DreamBook.Application.Users;
using System.Threading.Tasks;

namespace DreamBook.API.Auth
{
    public interface IAuthService
    {
        Task<(ApplicationUser User, UserResponseModel ResponseModel)> Register(UserRegisterModel model, params string[] roles);
        Task<AuthSucceededResponce> Authenticate(LoginModel model);
        Task<AuthSucceededResponce> GoogleAuthentication(GoogleAuthRequest googleAuth);
        Task<bool> RevokeRefreshToken(string token);
        Task<JwtTokenResponse> RefreshToken(string token);
    }
}
