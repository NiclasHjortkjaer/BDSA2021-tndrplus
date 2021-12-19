using ProjectBank.Core;

namespace ProjectBank.Client;

public interface IKeywordFinder
{
    IDictionary<string, int> Ratios { get; }
    Task Setup(HttpClient Http);
    string FindWeightedRandomKeyword();
    Status UpdateRatioAsync(string keywordName, bool userLikedProject);
    Task<ProjectDetailsDto?> ReadProjectGivenKeywordAsync(string keyword);
}