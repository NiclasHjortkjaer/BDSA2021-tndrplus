using ProjectBank.Server.Extensions;

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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<ProjectDto>> Get()
        => await _repository.ReadAllAsync();

    /*[AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectDetailsDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ProjectDetailsDto>? Get(int projectId)
        => await _repository.ReadAsync(projectId);*/

    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDetailsDto>), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{title}")]
    public async Task<IReadOnlyCollection<ProjectDetailsDto>>? Get(string title)
        => await _repository.ReadTitleAsync(title);

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ProjectDetailsDto), 201)]
    public async Task<IActionResult> Post(ProjectCreateDto project)
    {
        var created = await _repository.CreateAsync(project);

        return CreatedAtRoute(nameof(Get), new { created.Id }, created);
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Put(int id, [FromBody] ProjectUpdateDto project)
        => (await _repository.UpdateAsync(id, project)).ToActionResult();

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}