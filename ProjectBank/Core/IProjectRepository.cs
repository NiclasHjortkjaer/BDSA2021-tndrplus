namespace ProjectBank.Core;

public interface IProjectRepository
{
    Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto project);
    Task<ProjectDetailsDto> ReadAsync(int projectId);
    Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync();
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadTitleAsync(string input);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAuthorAsync(string input);
    
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadKeywordAsync(string input);
    Task<Status> UpdateAsync(int id, ProjectUpdateDto project);
    Task<Status> DeleteAsync(int projectId);
}