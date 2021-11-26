namespace ProjectBank.Infrastructure;

public class KeywordRepository : IKeywordRepository
{
    private readonly IProjectBankContext _context;

    public KeywordRepository(IProjectBankContext context)
    {
        _context = context;
    }
    public Task<KeywordDto> CreateAsync(KeywordCreateDto keyword)
    {
        throw new NotImplementedException();
    }

    public Task<KeywordDto> ReadAsync(int keywordId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<KeywordDto>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(int id, KeywordUpdateDto keyword)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int keywordId)
    {
        throw new NotImplementedException();
    }
}
