namespace DreamBook.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class LanguagesController : ControllerBase
{
    public AppLanguageManager Service { get; }
    public IMapper Mapper { get; }

    public LanguagesController(AppLanguageManager service, IMapper mapper)
    {
        Service = service;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LanguageResponseModel>>> All()
    {
        return Ok(await Task.FromResult(Mapper.Map<IEnumerable<LanguageResponseModel>>(Service.SupportLanguages)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LanguageResponseModel>> ById([FromRoute] Guid id)
    {
        return Ok(await Task.FromResult(Mapper.Map<LanguageResponseModel>(Service.SupportLanguages.FirstOrDefault(l => l.Guid == id))));
    }
}
