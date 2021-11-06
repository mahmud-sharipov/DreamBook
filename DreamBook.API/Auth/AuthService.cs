using DreamBook.API.Auth.Model;
using DreamBook.API.Auth.Requests;
using DreamBook.API.Auth.Responses;
using DreamBook.API.Persistence;
using DreamBook.Application.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.API.Auth
{
    public interface IAuthService
    {
        Task<ApplicationUser> Register(UserRegisterModel model, params string[] roles);
        Task<AuthSucceededResponce> Authenticate(LoginModel model);
        Task<AuthSucceededResponce> GoogleAuthentication(GoogleAuthRequest googleAuth);
        Task<bool> RevokeRefreshToken(string token);
        Task<JwtTokenResponse> RefreshToken(string token);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DreamBookIdentityContext _dbContext;
        private readonly TokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserService userService, IHttpContextAccessor httpContextAccessor, DreamBookIdentityContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _tokenService = new TokenService(configuration, userManager, dbContext, httpContextAccessor);
        }

        public async Task<ApplicationUser> Register(UserRegisterModel model, params string[] roles)
        {
            var user = await _userService.Create(model);

            ApplicationUser authUser = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = user.ToString(),
                UserName = model.Username,
                Id = user.Guid.ToString()
            };
            var result = await _userManager.CreateAsync(authUser, model.Password);

            if (!result.Succeeded)
            {
                await _userService.Delete(user.Guid);
                throw new BadHttpRequestException("User creation failed! Please check user details and try again.");
            }

            foreach (var role in roles)
                await _userManager.AddToRoleAsync(authUser, role);

            return authUser;
        }

        public async Task Logout()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            // Revoke Refresh Token 
            await _tokenService.RevokeRefreshToken("", null);
        }

        public async Task<AuthSucceededResponce> Authenticate(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthSucceededResponce(await _tokenService.GenerateTokens(user), user);

            return null;
        }

        public async Task<AuthSucceededResponce> GoogleAuthentication(GoogleAuthRequest googleAuth)
        {
            var googlePayload = await _tokenService.VerifyGoogleToken(googleAuth);
            if (googlePayload == null)
                return null;

            var info = new UserLoginInfo(googleAuth.Provider, googlePayload.Subject, googleAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(googlePayload.Email);
                if (user == null)
                    user = await Register(new UserRegisterModel() { Email = googlePayload.Email, Username = googlePayload.Email }, UserRoles.User);

                await _userManager.AddLoginAsync(user, info);
            }

            return new AuthSucceededResponce(await _tokenService.GenerateTokens(user), user);
        }

        public async Task<JwtTokenResponse> RefreshToken(string token)
        {
            var identityUser = _dbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));
            return await _tokenService.RefreshToken(token, identityUser);
        }

        public async Task<bool> RevokeRefreshToken(string token)
        {
            var user = _dbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));

            if (user == null) return false;

            // Revoke Refresh token
            await _tokenService.RevokeRefreshToken(token, user);
            return true;
        }
    }
}
