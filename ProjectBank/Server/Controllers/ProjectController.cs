namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProjectController : ControllerBase {
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectRepository _repository;

    public ProjectController(ILogger<ProjectController> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }  
}