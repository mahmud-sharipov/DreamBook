using DreamBook.API.Auth;
using DreamBook.API.Auth.Requests;
using DreamBook.API.Auth.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DreamBook.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            await _authService.Register(model, UserRoles.User);
            return Ok();
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterModel model)
        {
            await _authService.Register(model, UserRoles.User, UserRoles.Admin);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthSucceededResponce>> Login([FromBody] LoginModel model)
        {
            var user = await _authService.Authenticate(model);
            return user == null ? Unauthorized() : Ok(user);
        }

        [HttpPost]
        [Route("login/external/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest googleAuth)
        {
            var user = await _authService.GoogleAuthentication(googleAuth);
            return user == null ? Unauthorized() : Ok(user);
        }

        [HttpPost]
        [Route("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            string token = "";
            if (await _authService.RevokeRefreshToken(token))
            {
                return Ok(new { Message = "Success" });
            }

            return new BadRequestObjectResult(new { Message = "Failed" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token/{refreshToken}")]
        public async Task<IActionResult> RefreshToken([FromRoute] string refreshToken)
        {
            return Ok(await _authService.RefreshToken(refreshToken));
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok("Logged Out");
        }
    }
}
