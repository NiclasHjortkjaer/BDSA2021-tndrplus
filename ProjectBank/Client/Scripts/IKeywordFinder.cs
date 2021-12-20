using ProjectBank.Core.Enum;
using ProjectBank.Core.DTO;

namespace ProjectBank.Client.Scripts;

public interface IKeywordFinder
{
    IDictionary<string, int> Ratios { get; }
    Task Setup(HttpClient Http, Degree degree);
    string FindWeightedRandomKeyword();
    Status UpdateRatioAsync(string keywordName, bool userLikedProject);
    Task<ProjectDetailsDto?> ReadProjectGivenKeywordAsync(string keyword);
}