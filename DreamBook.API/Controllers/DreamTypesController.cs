using DreamBook.Application.DreamTypes;

namespace DreamBook.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class DreamTypesController : ControllerBase
{
    public IDreamTypeService Service { get; }

    public DreamTypesController(IDreamTypeService service)
    {
        Service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<DreamTypeResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<DreamTypeTranslation> requestModel)
    {
        return Ok(await Service.GetPagedList(requestModel));
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<DreamTypeResponseModel>>> GetAll()
    {
        return Ok(await Service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DreamTypeResponseModel>> GetById([FromRoute] Guid id)
    {
        return Ok(await Service.GetById(id));
    }

    [HttpGet("with-all-translations/{id}")]
    public async Task<ActionResult<DreamTypeWithTranslationsResponseModel>> GetByIdWithAllTranslations([FromRoute] Guid id)
    {
        return Ok(await Service.GetByIdWithTranslations(id));
    }

    [HttpPost]
    [RequireModerator]
    public async Task<ActionResult<DreamTypeResponseModel>> Create([FromBody] CreateDreamTypeRequestModel model)
    {
        var responce = await Service.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
    }

    [HttpPut]
    [RequireModerator]
    public async Task<IActionResult> Update([FromBody] UpdateDreamTypeRequestModel model)
    {
        await Service.Update(model);
        return NoContent();
    }

    [RequireModerator]
    [HttpDelete("{id}")]
    public async virtual Task<IActionResult> Delete(Guid id)
    {
        await Service.Delete(id);
        return NoContent();
    }
}
