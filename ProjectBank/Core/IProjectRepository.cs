namespace ProjectBank.Core;

public interface IProjectRepository
{
    Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto project);
    Task<ProjectDetailsDto> ReadAsync(int projectId);
    Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, ProjectUpdateDto project);
    Task<Status> DeleteAsync(int projectId);
}