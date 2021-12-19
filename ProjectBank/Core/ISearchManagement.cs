namespace ProjectBank.Core;

public interface ISearchManagement
{
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString, Degree degree);
}