namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProjectController : ControllerBase {
    private readonly IProjectRepository _repository;

    public ProjectController(IProjectRepository repository)
    {
        _repository = repository;
    }  

    [AllowAnonymous]
    [HttpGet]
    public Task<IReadOnlyCollection<ProjectDto>> Get()
        => _repository.ReadAllAsync();

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProjectDetailsDto), 200)]
    [ProducesResponseType(404)]
    public Task<ProjectDetailsDto?> Get(int id)
        => _repository.ReadAsync(id);

    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProjectDetailsDto>), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{input}")]
    public async Task<IReadOnlyCollection<ProjectDetailsDto>> Get(string input) {
        var projectsByTitle = await _repository.ReadTitleAsync(input);
        var projectsByAuthor = await _repository.ReadAuthorAsync(input);

        return projectsByTitle.Union(projectsByAuthor).ToList().AsReadOnly();
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ProjectDetailsDto), 201)]
    public async Task<IActionResult> Post(ProjectCreateDto project)
    {
        var created = await _repository.CreateAsync(project);
        CreatedAtAction(nameof(Get), new {created.Id}, created);

        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [Authorize]
    [HttpPut("{id:int}")]
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