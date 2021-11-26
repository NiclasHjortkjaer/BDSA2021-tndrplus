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

    public Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(int id, ProjectUpdateDto project)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int projectId)
    {
        throw new NotImplementedException();
    }
}
