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
        var entity = await _context.Keywords
            .Include("Projects.Author")
            .FirstOrDefaultAsync(e => e.Word == keyword.Word);
        if (entity == null)
        {
            return new List<ProjectDto>().AsReadOnly();
        }
        var list = new List<ProjectDto>();
        foreach (var p in entity.Projects)
        {
            list.Add( new ProjectDto(p.Id, p.Author!.AzureAdToken, p.Author.Name, p.Title, p.Description));
        }
        return list.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordStringAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return new List<ProjectDetailsDto>();
        } else {
            var entity = await _context.Keywords
                //.Include(k => k.Projects.Select(p => p.Author)) Should work this way. However it does not
                .Include("Projects.Author") //Eager load multople levels. Use string to specify reltaionship
                .FirstOrDefaultAsync(e => e.Word == input);
            if (entity == null)
            {
                return new List<ProjectDetailsDto>().AsReadOnly();
            }

            var list = new List<ProjectDetailsDto>();
            foreach (var p in entity.Projects)
            {
                ISet<string> keywords = p.Keywords.Select(k => k.Word).ToHashSet();
                list.Add(new ProjectDetailsDto(
                    p.Id, p.Author?.AzureAdToken, p.Author?.Name, p.Title, p.Description ,p.Degree, p.ImageUrl ,p.FileUrl, p.Ects, p.LastUpdated,keywords));
            }
            return list.AsReadOnly();
        }
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
