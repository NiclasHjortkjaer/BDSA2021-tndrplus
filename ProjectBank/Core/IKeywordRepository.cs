namespace ProjectBank.Core;

public interface IKeywordRepository
{
    Task<KeywordDto> CreateAsync(KeywordCreateDto keyword);
    Task<KeywordDetailsDto?> ReadAsync(int keywordId);
    Task<IReadOnlyCollection<KeywordDetailsDto>> ReadAllAsync();
    Task<IReadOnlyCollection<string>> ReadAllWordsAsync();
    Task<Status> DeleteAsync(int keywordId);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordStringAsync(string keyword);
    Task<int> ReadNumberOfProjectsGivenKeywordAndDegree(string keyword, Degree degree);
    Task<ProjectDetailsDto> ReadProjectGivenKeywordAndTimesSeenRandAsync(string keyword, int timesSeen, Degree degree);
    Task<ProjectDetailsDto> ReadProjectGivenKeywordAndTimesSeenAsync(string keyword, int timesSeen, Degree degree = Degree.Unspecified);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordAndDegreeAsync(string keyword, Degree degree = Degree.Unspecified);
    Task<int> ReadNumberOfProjectsGivenKeyword(string keyword);
}