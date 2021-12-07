using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using SQLitePCL;

namespace ProjectBank.Infrastructure;

public class KeywordRepository : IKeywordRepository
{
    private readonly IProjectBankContext _context;

    public KeywordRepository(IProjectBankContext context)
    {
        _context = context;
    }
    public async Task<KeywordDto> CreateAsync(KeywordCreateDto keyword)
    {
        var conflict = await _context.Keywords
            .Where(k => k.Word == keyword.Word)
            .Select(k => new KeywordDto(k.Id, k.Word))
            .FirstOrDefaultAsync();

        if (conflict != null)
        {
            return null!;
        }
        var entity = new Keyword(keyword.Word);
        _context.Keywords.Add(entity);
        await _context.SaveChangesAsync();
        return new KeywordDto(entity.Id, entity.Word);
    }

    public async Task<KeywordDto> ReadAsync(int keywordId)
    {
        var keywords = from k in _context.Keywords
            where k.Id == keywordId
            select new KeywordDto(
                k.Id,
                k.Word
            );

        return await keywords.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<KeywordDto>> ReadAllAsync() =>
        (await _context.Keywords
            .Select(k => new KeywordDto(k.Id, k.Word))
            .ToListAsync())
            .AsReadOnly();

    public async Task<IReadOnlyCollection<ProjectDto>> ReadAllProjectsWithKeywordAsync(KeywordDto keyword)
    {
        var projects = await _context.Keywords
            .Where(k => k.Word == keyword.Word)
            .Select(k => k.Projects).ToListAsync();
        
        var list = new List<ProjectDto>();
        foreach (var p in projects[0])
        {
            list.Add( new ProjectDto(p.Id, p.Author!.AzureAdToken, p.Author!.FirstName, p.Author!.LastName, p.Title, p.Description));
        }

        return list.AsReadOnly();
    }



    /* public async Task<Status> UpdateAsync(int id, KeywordUpdateDto keyword)
    {
        var conflict = await _context.Keywords
            .Where(k => k.Id != keyword.Id)
            .Where(k => k.Word == keyword.Word)
            .Select(k => new KeywordDto(k.Id, k.Word))
            .AnyAsync();
        if (conflict)
        {
            return Status.Conflict;
        }

        var entity = await _context.Keywords.FirstOrDefaultAsync(k => k.Id == id);
        
        if (entity == default)
        {
            return Status.NotFound;
        }

        entity.Word = keyword.Word;
        await _context.SaveChangesAsync();
        return Status.Updated;
    } */

    public async Task<Status> DeleteAsync(int keywordId)
    {
        var keyword = 
            await _context.Keywords
                .Include(k => k.Projects)
                .FirstOrDefaultAsync(k => k.Id == keywordId);
        if (keyword == null)
        {
            return Status.NotFound;
        }

        if (keyword.Projects.Any())
        {
            return Status.Conflict;
        }
        _context.Keywords.Remove(keyword);
        await _context.SaveChangesAsync();
        
        return Status.Deleted;
    }
    private async IAsyncEnumerable<Project> GetSavedProjectsAsync(IEnumerable<string> projects)
    {
        var existing = await _context.Projects
            .Where(p => projects.Contains(p.Title))
            .ToDictionaryAsync(p => p.Title);

        foreach (var project in projects)
        {
            yield return existing.TryGetValue(project, out var p) ? p : new Project(project);
        }
    }
}
