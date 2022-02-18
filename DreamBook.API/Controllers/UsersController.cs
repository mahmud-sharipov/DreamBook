using DreamBook.Application.Users.RequestModels;

namespace DreamBook.API.Controllers;

[ApiController]
[Authorize]
[RequireAdmin]
[ApiVersion("1.0")]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService<User> _userService;
    private readonly UserManager<User> _userManager;

    public UsersController(IUserService<User> userService, UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<UserResponseModel>>> GetPagedList([FromQuery] UserPagedListRequestModel requestModel)
    {
        return Ok(await _userService.GetPagedList(requestModel));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UserResponseModel>> GetById([FromRoute] Guid id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseModel>> Create([FromBody] CreateUserRequestModel model)
    {
        var responce = await _userService.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequestModel model)
    {
        await _userService.Update(model);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async virtual Task<IActionResult> Delete(Guid id)
    {
        await _userService.Delete(id);
        return NoContent();
    }

    [HttpPut]
    [Route("username")]
    public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserUsernameRequestModel requestModel)
    {
        var userId = HttpContext.User.FindFirst(TokenService.UserIdClaim).Value;
        requestModel.Guid = Guid.Parse(userId);
        await _userService.UpdateUsername(requestModel);
        return Ok();
    }

    #region Roles
    [HttpGet("{id:Guid}/roles")]
    public async Task<ActionResult<IList<string>>> GetRoles([FromRoute] Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound(id);
        return Ok(await _userManager.GetRolesAsync(user));
    }

    [HttpPost("{id:Guid}/roles")]
    public async Task<IActionResult> AddRole([FromRoute] Guid id, [FromBody] UserRole role)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound(id);

        await _userManager.AddToRoleAsync(user, role.ToString());
        return NoContent();
    }

    [HttpPut("{id:Guid}/roles")]
    public async Task<IActionResult> ReplaceRole([FromRoute] Guid id, [FromBody] ReplaceRoleRequlesModel model)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound(id);

        await _userManager.RemoveFromRoleAsync(user, model.OldRole.ToString());
        await _userManager.AddToRoleAsync(user, model.NewRole.ToString());

        return NoContent();
    }

    [HttpDelete("{id:Guid}/roles")]
    public async virtual Task<IActionResult> RemoveRole([FromRoute] Guid id, [FromBody] UserRole role)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return NotFound(id);
        await _userManager.RemoveFromRoleAsync(user, role.ToString());
        return NoContent();
    }

    [HttpGet("roles")]
    public virtual ActionResult<string[]> GetAllRoles()
    {
        return Ok(new[] { UserRoles.Admin, UserRoles.Moderator, UserRoles.Basic });
    }
    #endregion
}

public class ReplaceRoleRequlesModel
{
    public UserRole OldRole { get; set; }
    public UserRole NewRole { get; set; }
}