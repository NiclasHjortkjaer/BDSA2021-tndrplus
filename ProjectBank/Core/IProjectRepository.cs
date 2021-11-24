namespace ProjectBank.Core;

public interface IProjectRepository
{
    Task<ProjectDto> CreateAsync(ProjectCreateDto project);
    Task<KeywordDto> ReadAsync(int projectId);
    Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync();
    Task<Status> Update(int id, ProjectUpdateDto project);
    Task<Status> Delete(int projectId);
}