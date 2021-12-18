namespace ProjectBank.Core;

public interface IKeywordRepository
{
    Task<KeywordDto> CreateAsync(KeywordCreateDto keyword);
    Task<KeywordDto> ReadAsync(int keywordId);
    Task<IReadOnlyCollection<KeywordDto>> ReadAllAsync();

    Task<IReadOnlyCollection<ProjectDto>> ReadAllProjectsWithKeywordAsync(KeywordDto keyword);
    // Task<Status> UpdateAsync(int id, KeywordUpdateDto keyword);
    Task<Status> DeleteAsync(int keywordId);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordStringAsync(string keyword);
    Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordAndDegreeAsync(string keyword, Degree degree);
}