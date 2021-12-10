using ProjectBank.Server.Extensions;

namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class KeywordController : ControllerBase 
{
    private readonly ILogger<KeywordController> _logger;
    private readonly IKeywordRepository _repository;

    public KeywordController(ILogger<KeywordController> logger, IKeywordRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }    

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<KeywordDto>> Get()
        => await _repository.ReadAllAsync();


    [AllowAnonymous]
    [HttpGet("getby/{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(KeywordDto), 200)]
    public async Task<KeywordDto>? Get(int id)
       => await _repository.ReadAsync(id);


    [AllowAnonymous]
    [HttpGet("{keyword}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDto>), 200)]
    public async Task<IReadOnlyCollection<ProjectDetailsDto>> Get([FromRoute]string keyword)
        => await _repository.ReadAllProjectsWithKeywordStringAsync(keyword); 

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(KeywordDto), 201)]
    public async Task<IActionResult> Post(KeywordCreateDto keyword)
    {
        var created = await _repository.CreateAsync(keyword);

        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    /* [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Put(int id, [FromBody] KeywordUpdateDto keyword)
        => (await _repository.UpdateAsync(id, keyword)).ToActionResult(); */

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}