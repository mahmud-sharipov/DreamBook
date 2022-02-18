namespace DreamBook.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class DreamsController : ControllerBase
{
    public IDreamService Service { get; }

    public DreamsController(IDreamService service)
    {
        Service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<DreamResponseModel>>> GetPagedList([FromQuery] DreamPagedListRequestModel requestModel)
    {
        return Ok(await Service.GetPagedList(requestModel));
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<DreamResponseModel>>> GetAll()
    {
        return Ok(await Service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DreamResponseModel>> GetById([FromRoute] Guid id)
    {
        return Ok(await Service.GetById(id));
    }

    [AllowAnonymous]
    [HttpGet("shared")]
    public async Task<ActionResult<IPagedList<DreamResponseModel>>> GetAllShared([FromQuery] DreamPagedListRequestModel requestModel)
    {
        return Ok(await Service.GetAllShared(requestModel));
    }

    [AllowAnonymous]
    [HttpGet("shared/{id}")]
    public async Task<ActionResult<DreamResponseModel>> GetSharedById([FromRoute] Guid id)
    {
        return Ok(await Service.GetSharedById(id));
    }

    [HttpGet("recycle-bin")]
    public async Task<ActionResult<IEnumerable<DreamResponseModel>>> GetAllFromRecycleBin()
    {
        return Ok(await Service.GetAllFromRecycleBin());
    }

    [HttpDelete("recycle-bin/{id}")]
    public async Task<IActionResult> RestoreFromRecycleBin([FromRoute] Guid id)
    {
        await Service.RestoreFromRecycleBin(id);
        return Ok();
    }

    [HttpPut("recycle-bin/{id}")]
    public async Task<IActionResult> MoveToRecycleBin([FromRoute] Guid id)
    {
        await Service.MoveToRecycleBin(id);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<DreamResponseModel>> Create([FromBody] CreateDreamRequestModel model)
    {
        var responce = await Service.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDreamRequestModel model)
    {
        await Service.Update(model);
        return NoContent();
    }

    [HttpPut("{id}/words")]
    public async Task<IActionResult> AddWords([FromRoute] Guid id, [FromBody] IEnumerable<Guid> wordGuids)
    {
        await Service.AddWords(id, wordGuids);
        return NoContent();
    }

    [HttpDelete("{id}/words")]
    public async Task<IActionResult> RemoveWords([FromRoute] Guid id, [FromBody] IEnumerable<Guid> wordGuids)
    {
        await Service.RemoveWords(id, wordGuids);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async virtual Task<IActionResult> Delete(Guid id)
    {
        await Service.Delete(id);
        return NoContent();
    }
}
