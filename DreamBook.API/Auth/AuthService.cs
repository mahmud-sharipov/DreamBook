using DreamBook.API.Auth.Model;
using DreamBook.API.Auth.Requests;
using DreamBook.API.Auth.Responses;
using DreamBook.API.Persistence;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Exceptions;
using DreamBook.Application.Users;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DreamBook.API.Auth
{
    public class AuthService : IAuthService, IUserIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DreamBookIdentityContext _dbContext;
        private readonly IContext _appContext;
        private readonly TokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IUserService userService, IHttpContextAccessor httpContextAccessor, DreamBookIdentityContext dbContext, IContext appContext)
        {
            _userManager = userManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _appContext = appContext;
            _tokenService = new TokenService(configuration, userManager, dbContext, httpContextAccessor);
        }

        public async Task<(ApplicationUser User, UserResponseModel ResponseModel)> Register(UserRegisterModel model, params string[] roles)
        {
            var user = await _userService.Create(model);

            ApplicationUser authUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                Id = user.Guid
            };

            var result = await _userManager.CreateAsync(authUser, model.Password);
            if (!result.Succeeded)
            {
                await _userService.DeleteFull(user.Guid);
                throw new BusinessLogicException("User creation failed! Please check user details and try again.");
            }

            foreach (var role in roles)
                await _userManager.AddToRoleAsync(authUser, role);

            return (authUser, user);
        }

        public async Task<AuthSucceededResponse> Authenticate(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userResponseModel = await _userService.GetById(user.Id);
                return new AuthSucceededResponse(await _tokenService.GenerateTokens(user), userResponseModel);
            }

            return null;
        }

        public async Task<AuthSucceededResponse> GoogleAuthentication(GoogleAuthRequest googleAuth)
        {
            var googlePayload = await _tokenService.VerifyGoogleToken(googleAuth);
            if (googlePayload == null)
                return null;

            var info = new UserLoginInfo("google", googlePayload.Subject, "google");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            UserResponseModel userResponseModel = null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(googlePayload.Email);
                if (user == null)
                {
                    var registrationResult = await Register(new UserRegisterModel()
                    {
                        Gender = Domain.Enums.Gender.None,
                        FullName = googlePayload.Name,
                        Email = googlePayload.Email,
                        UserName = googlePayload.Email,
                        Password = RandonPassword(),
                        AvatarImage = ""
                    }, UserRoles.User);
                    user = registrationResult.User;
                    userResponseModel = registrationResult.ResponseModel;
                }
                await _userManager.AddLoginAsync(user, info);
            }

            if (userResponseModel == null)
                userResponseModel = await _userService.GetById(user.Id);
            return new AuthSucceededResponse(await _tokenService.GenerateTokens(user), userResponseModel);
        }

        public async Task<JwtTokenResponse> RefreshToken(string token)
        {
            var identityUser = _dbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token));
            return await _tokenService.RefreshToken(token, identityUser);
        }

        public async Task<bool> RevokeRefreshToken(string token)
        {
            var user = _dbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token));
            if (user == null) return false;

            await _tokenService.RevokeRefreshToken(token, user);
            return true;
        }

        public IUser GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appContext.GetFirstOrDefault<User>(u => u.UserName == userId);
        }

        public string RandonPassword()
        {
            string[] randomChars = new[]
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                "abcdefghijkmnopqrstuvwxyz",
                "0123456789",
                "!@$?_-"
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

            chars.Insert(rand.Next(0, chars.Count),
                randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < 8; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
