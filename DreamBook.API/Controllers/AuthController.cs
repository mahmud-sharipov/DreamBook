using DreamBook.API.Auth;
using DreamBook.API.Auth.Requests;
using DreamBook.API.Auth.Responses;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Users;
using DreamBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DreamBook.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IContext _appContext;

        public AuthController(IAuthService authService, IUserService userService, IContext appContext)
        {
            _authService = authService;
            _userService = userService;
            _appContext = appContext;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult<UserResponseModel>> Register([FromBody] UserRegisterModel model)
        {
            var reqult = await _authService.Register(model, UserRoles.User);
            return Ok(reqult.ResponseModel);
        }

        [HttpPost]
        [RequireAdmin]
        [Route("register-admin")]
        public async Task<ActionResult<UserResponseModel>> RegisterAdmin([FromBody] UserRegisterModel model)
        {
            var reqult = await _authService.Register(model, UserRoles.User, UserRoles.Admin);
            return Ok(reqult.ResponseModel);
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
        [Route("revoke-token")]
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

        //[HttpPut]
        //[Route("me/username")]
        //public async Task<IActionResult> UpdateUserName([FromBody] string newUserName)
        //{
        //    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var user = _appContext.GetFirstOrDefault<User>(u => u.UserName == userId);
        //    await _userService.Update(user.Guid, requestModel);
        //    return Ok();
        //}
    }
}
