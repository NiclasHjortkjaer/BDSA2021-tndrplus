using ProjectBank.Core.DTO;
using ProjectBank.Core.Enum;

namespace ProjectBank.Core.RepositoryInterface;

public interface ISearchManagement
{
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString, Degree degree);
}