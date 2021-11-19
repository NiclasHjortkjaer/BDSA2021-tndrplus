namespace Core;

public interface IProjectRepository
{
    Task<ProjectDto> CreateAsync(ProjectCreateDto project);
    Task<Option<KeywordDto>> ReadAsync(int projectId);
    Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync();
    Task<Status> Update(int id, ProjectUpdateDto project);
    Task<Status> Delete(int projectId);
}