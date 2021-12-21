using ProjectBank.Infrastructure.Entity;

namespace ProjectBank.Infrastructure.Repository;

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

public Task<KeywordDetailsDto?> ReadAsync(int keywordId)
    {
        var keywords = from k in _context.Keywords
            where k.Id == keywordId
            select new KeywordDetailsDto(
                k.Id,
                k.Word,
                k.Projects.Select(p => p.Title).ToHashSet()
            );

        return keywords.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<KeywordDetailsDto>> ReadAllAsync() =>
        (await _context.Keywords
            .Select(k => new KeywordDetailsDto(k.Id, k.Word, k.Projects.Select(p => p.Title).ToHashSet()))
            .ToListAsync())
            .AsReadOnly();

    public async Task<IReadOnlyCollection<string>> ReadAllWordsAsync()
        => (await _context.Keywords
                .Select(k => k.Word)
                .ToListAsync())
                .AsReadOnly();
    
    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordStringAsync(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return new List<ProjectDetailsDto>();

        }
        var entity = await _context.Keywords
            //.Include(k => k.Projects.Select(p => p.Author)) Should work this way. However it does not
            .Include("Projects.Author") //Eager load multople levels. Use string to specify reltaionship
            .Include("Projects.Keywords")
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
   
    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAllProjectsWithKeywordAndDegreeAsync(string input, Degree degree = Degree.Unspecified)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return new List<ProjectDetailsDto>();
        } 
        var entity = await _context.Keywords
            .Include("Projects.Author")
            .FirstOrDefaultAsync(e => e.Word == input);
        
        if (entity == null)
        {
            return new List<ProjectDetailsDto>().AsReadOnly();
        }

        var list = new List<ProjectDetailsDto>();
        
        if (degree == Degree.Unspecified) //The default parameter value of degree is Unspecified, so if no specific degree is given, we just pick em all matching the keyword
        {
            foreach (var p in entity.Projects)
            {
            
                ISet<string> keywords = p.Keywords.Select(k => k.Word).ToHashSet();
                list.Add(new ProjectDetailsDto(
                    p.Id, p.Author?.AzureAdToken, p.Author?.Name, p.Title, p.Description ,p.Degree, p.ImageUrl ,p.FileUrl, p.Ects, p.LastUpdated,keywords));
            }
        }
        else
        {
            foreach (var p in entity.Projects)
            {
                if (p.Degree == degree)
                {
                    ISet<string> keywords = p.Keywords.Select(k => k.Word).ToHashSet();
                    list.Add(new ProjectDetailsDto(
                        p.Id, p.Author?.AzureAdToken, p.Author?.Name, p.Title, p.Description ,p.Degree, p.ImageUrl ,p.FileUrl, p.Ects, p.LastUpdated,keywords));
                }
            } 
        }
        return list.AsReadOnly();
    
    }        

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

    public async Task<ProjectDetailsDto> ReadProjectGivenKeywordAndTimesSeenRandAsync(string keyword, int timesSeen, Degree degree) 
    {
        var project = await ReadProjectGivenKeywordAndTimesSeenAsync(keyword, timesSeen, degree);
        if (project != null)
        {
            return project;
        }

        //Returns random project, when there are no more projects with the given keyword
        //Does not promise, not to show an already shown project
        Random rand = new Random();
        var randomIndex = rand.Next(1, _context.Projects.Count());
        var projects = from p in _context.Projects
            where p.Id == randomIndex
            select new ProjectDetailsDto(
                p.Id,
                p.Author == null ? null : p.Author.AzureAdToken,
                p.Author == null ? null : p.Author.Name,
                p.Title,
                p.Description,
                p.Degree,
                p.ImageUrl,
                p.FileUrl,
                p.Ects,
                p.LastUpdated,
                p.Keywords.Select(k => k.Word).ToHashSet()
            );

        return projects.FirstOrDefault()!;
    }
    
    public async Task<ProjectDetailsDto> ReadProjectGivenKeywordAndTimesSeenAsync(string keyword, int timesSeen, Degree degree = Degree.Unspecified) 
    {
        var entity = await _context.Keywords
            .Include("Projects.Author")
            .Include("Projects.Keywords")
            .FirstOrDefaultAsync(e => e.Word == keyword);
       
        List<Project> keyProjects;
        if (entity == null)
        {
            return null; //skal vi lave lidt error handling på nulls i denne sammenhæng
        }
        
        if (degree == Degree.Unspecified)
        {
            keyProjects = entity.Projects.ToList();
        }
        else
        {
           keyProjects = entity.Projects.Where(p => p.Degree == degree).ToList();
        }

        if (keyProjects.Count <= 0 || timesSeen >= keyProjects.Count) return null;
        
        var p = keyProjects.ElementAt(timesSeen);
        ISet<string> keywords = p.Keywords.Select(k => k.Word).ToHashSet();
    
        return new ProjectDetailsDto(
        p.Id, p.Author?.AzureAdToken, p.Author?.Name, p.Title, p.Description, p.Degree, p.ImageUrl, p.FileUrl, p.Ects, p.LastUpdated, keywords);

    }
}
