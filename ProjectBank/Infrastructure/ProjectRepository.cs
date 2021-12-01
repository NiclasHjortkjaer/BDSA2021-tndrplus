namespace ProjectBank.Infrastructure;
public class ProjectRepository : IProjectRepository
{
    private readonly IProjectBankContext _context;

    public ProjectRepository(IProjectBankContext context)
    {
        _context = context;
    }
    public Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto project)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDetailsDto> ReadAsync(int projectId)
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
