namespace DreamBook.API.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService<User> _userService;
    private readonly IContext _appContext;

    public AuthController(IAuthService authService, IUserService<User> userService, IContext appContext)
    {
        _authService = authService;
        _userService = userService;
        _appContext = appContext;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<ActionResult<AuthSucceededResponse>> Login([FromBody] LoginModel model)
    {
        var user = await _authService.Authenticate(model);
        return user == null ? Unauthorized() : Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login/external/google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest googleAuth)
    {
        var user = await _authService.GoogleAuthentication(googleAuth);
        return user == null ? Unauthorized() : Ok(user);
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
    {
        if (await _authService.RevokeRefreshToken(refreshToken))
            return Ok(new { Message = "Success" });

        return new BadRequestObjectResult(new { Message = "Failed" });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        return Ok(await _authService.RefreshToken(refreshToken));
    }

    [HttpGet]
    [Route("me")]
    public async Task<ActionResult<UserResponseModel>> GetCurrentUserProfile()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(await _userService.GetByUserName(userId));
    }

    [HttpPut]
    [Route("me")]
    public async Task<IActionResult> UpdateCurrentUserProfile([FromBody] UserRequestModel requestModel)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = _appContext.GetFirstOrDefault<User>(u => u.UserName == userId);
        await _userService.Update(user.Guid, requestModel);
        return Ok();
    }

    [HttpPut]
    [Route("me/username")]
    public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserUsernameRequestModel requestModel)
    {
        var userId = HttpContext.User.FindFirst(TokenService.UserIdClaim).Value;
        requestModel.Guid = Guid.Parse(userId);
        await _userService.UpdateUsername(requestModel);
        return Ok();
    }

}
