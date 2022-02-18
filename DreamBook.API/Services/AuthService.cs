namespace DreamBook.API.Services;

public class AuthService : IAuthService, IUserIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService<User> _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IContext _appContext;
    private readonly TokenService _tokenService;

    public AuthService(UserManager<User> userManager, IConfiguration configuration, IUserService<User> userService, IHttpContextAccessor httpContextAccessor, IContext appContext)
    {
        _userManager = userManager;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
        _appContext = appContext;
        _tokenService = new TokenService(configuration, userManager, appContext, httpContextAccessor);
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
                userResponseModel = await _userService.Create(new CreateUserRequestModel()
                {
                    Gender = Gender.None,
                    FullName = googlePayload.Name,
                    Email = googlePayload.Email,
                    UserName = googlePayload.Email,
                    Password = RandonPassword(),
                    AvatarImage = ""
                });
                user = await _userManager.FindByIdAsync(userResponseModel.Guid.ToString());
            }
            await _userManager.AddLoginAsync(user, info);
        }

        if (userResponseModel == null)
            userResponseModel = await _userService.GetById(user.Id);
        return new AuthSucceededResponse(await _tokenService.GenerateTokens(user), userResponseModel);
    }

    public async Task<JwtTokenResponse> RefreshToken(string token)
    {
        var identityUser = _userManager.Users.Include(x => x.RefreshTokens)
            .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token));
        return await _tokenService.RefreshToken(token, identityUser);
    }

    public async Task<bool> RevokeRefreshToken(string token)
    {
        var user = _userManager.Users.Include(x => x.RefreshTokens)
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
