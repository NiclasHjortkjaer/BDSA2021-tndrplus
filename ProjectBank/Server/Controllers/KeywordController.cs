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
    public Task<IReadOnlyCollection<KeywordDetailsDto>> Get()
        => _repository.ReadAllAsync();

    [AllowAnonymous]
    [HttpGet("getStrings")]
    public Task<IReadOnlyCollection<string>> GetKeywordStrings()
        => _repository.ReadAllWordsAsync();

    [AllowAnonymous]
    [HttpGet("getby/{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(KeywordDetailsDto), 200)]
    public Task<KeywordDetailsDto>? Get(int id)
       => _repository.ReadAsync(id);

    [AllowAnonymous]
    [HttpGet("typeOption/{keyword}/{timesSeen}/{degree}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ProjectDetailsDto), 200)]
    public Task<ProjectDetailsDto>? Get(string keyword, int timesSeen, [FromRoute] Degree degree)
       => _repository.ReadProjectGivenKeywordAndTimesSeenAsync(keyword, timesSeen, degree);
    
    [AllowAnonymous]
    [HttpGet("{keyword}/{timesSeen}/{degree}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ProjectDetailsDto), 200)]
    public Task<ProjectDetailsDto>? GetBrowseAll(string keyword, int timesSeen, Degree degree)
        => _repository.ReadProjectGivenKeywordAndTimesSeenRandAsync(keyword, timesSeen, degree);

    [AllowAnonymous]
    [HttpGet("{keyword}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDto>), 200)]
    public Task<IReadOnlyCollection<ProjectDetailsDto>> Get([FromRoute]string keyword)
        => _repository.ReadAllProjectsWithKeywordStringAsync(keyword); 

    [HttpGet("withType/{keyword}/{degree}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDto>), 200)]
    public Task<IReadOnlyCollection<ProjectDetailsDto>> Get([FromRoute]string keyword, [FromRoute] Degree degree) //hvad gør FromRoute lige? vi har ik gjort det alle steder?? -carl
        => _repository.ReadAllProjectsWithKeywordAndDegreeAsync(keyword, degree); 

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(KeywordDto), 201)]
    public async Task<IActionResult> Post(KeywordCreateDto keyword)
    {
        var created = await _repository.CreateAsync(keyword);
        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}