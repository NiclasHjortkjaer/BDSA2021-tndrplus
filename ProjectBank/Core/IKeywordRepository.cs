namespace Core;

public interface IKeywordRepository
{
    Task<KeywordDto> CreateAsync(KeywordCreateDto keyword);
    Task<Option<KeywordDto>> ReadAsync(int keywordId);
    Task<IReadOnlyCollection<KeywordDto>> ReadAllAsync();
    Task<Status> Update(int id, KeywordUpdateDto keyword);
    Task<Status> Delete(int keywordId);
}