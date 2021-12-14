namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class SearchQueryController : ControllerBase {
    private readonly ILogger<SearchQueryController> _logger;
    private readonly ISearchManagement _management;

    public SearchQueryController(ILogger<SearchQueryController> logger, ISearchManagement management)
    {
        _logger = logger;
        _management = management;
    }

    [AllowAnonymous]
    [HttpGet]
    public IReadOnlyCollection<ProjectDto> Get()
        => new List<ProjectDetailsDto>().AsReadOnly();

    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDetailsDto>), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{input}")]
    public async Task<IReadOnlyCollection<ProjectDetailsDto>>? Get(string input) 
        => await _management.ReadSearchQueryAsync(input);
        
}