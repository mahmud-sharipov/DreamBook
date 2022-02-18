namespace DreamBook.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AdsController : ControllerBase
{
    public IAdService Service { get; }

    public AdsController(IAdService service)
    {
        Service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IPagedList<AdResponseModel>>> GetPagedList([FromQuery] PagedListRequestModel<AdTranslation> requestModel)
    {
        return Ok(await Service.GetPagedList(requestModel));
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<AdResponseModel>>> GetAll()
    {
        return Ok(await Service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdResponseModel>> GetById([FromRoute] Guid id)
    {
        return Ok(await Service.GetById(id));
    }

    [HttpGet("with-all-translations/{id}")]
    public async Task<ActionResult<AdWithTranslationsResponseModel>> GetByIdWithAllTranslations([FromRoute] Guid id)
    {
        return Ok(await Service.GetByIdWithTranslations(id));
    }

    [HttpPost]
    [RequireModerator]
    public async Task<ActionResult<AdResponseModel>> Create([FromBody] CreateAdRequestModel model)
    {
        var responce = await Service.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = responce.Guid }, responce);
    }

    [HttpPut]
    [RequireModerator]
    public async Task<IActionResult> Update([FromBody] UpdateAdRequestModel model)
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
