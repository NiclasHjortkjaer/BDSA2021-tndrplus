namespace ProjectBank.Infrastructure;
public class ProjectRepository : IProjectRepository
{
    private readonly IProjectBankContext _context;

    public ProjectRepository(IProjectBankContext context)
    {
        _context = context;
    }
    public Task<ProjectDto> CreateAsync(ProjectCreateDto project)
    {
        throw new NotImplementedException();
    }

    public Task<KeywordDto> ReadAsync(int projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync() =>
        (await _context.Projects
            .Select(p => new ProjectDto(p.Id, p.Author.AzureAdToken, p.Title, p.Description))
            .ToListAsync())
            .AsReadOnly();

    public Task<Status> UpdateAsync(int id, ProjectUpdateDto project)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int projectId)
    {
        throw new NotImplementedException();
    }
}
