namespace ProjectBank.Core;

public interface ISearchManagement
{
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string input);
}