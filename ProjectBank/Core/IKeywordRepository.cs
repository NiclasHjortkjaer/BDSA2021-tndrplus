namespace ProjectBank.Core;

public interface IKeywordRepository
{
    Task<KeywordDto> CreateAsync(KeywordCreateDto keyword);
    Task<KeywordDto> ReadAsync(int keywordId);
    Task<IReadOnlyCollection<KeywordDto>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, KeywordUpdateDto keyword);
    Task<Status> DeleteAsync(int keywordId);
}