using ProjectBank.Core;

namespace ProjectBank.Client;

public interface IKeywordFinder
{
    Task Setup(HttpClient Http);
    string FindWeightedRandomKeyword();
    Status UpdateRatioAsync(string keywordName, bool userLikedProject);
    Task<ProjectDetailsDto?> ReadProjectGivenKeywordAsync(string keyword);
}