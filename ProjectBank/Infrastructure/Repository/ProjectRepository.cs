using ProjectBank.Infrastructure.Entity;

namespace ProjectBank.Infrastructure.Repository;
public class ProjectRepository : IProjectRepository
{
    private readonly IProjectBankContext _context;

    public ProjectRepository(IProjectBankContext context)
    {
        _context = context;
    }

    public async Task<ProjectDetailsDto> CreateAsync(ProjectCreateDto project)
    {
        var newProject = new Project(project.Title)
        {
            Description = project.Description,
            FileUrl = project.FileUrl,
            LastUpdated = project.LastUpdated,
            Degree = project.Degree,
            Ects = project.Ects,
            ImageUrl = project.ImageUrl,
            Author = await GetAuthorAsync(project.AuthorToken, project.AuthorName),
            Keywords = await GetKeywordsAsync(project.Keywords).ToListAsync()
        };
        _context.Projects.Add(newProject);
        await _context.SaveChangesAsync();

        return new ProjectDetailsDto(
            newProject.Id,
            newProject.Author?.AzureAdToken,
            newProject.Author?.Name,
            newProject.Title,
            newProject.Description,
            newProject.Degree,
            newProject.ImageUrl,
            newProject.FileUrl,
            newProject.Ects,
            newProject.LastUpdated,
            newProject.Keywords.Select(k => k.Word).ToHashSet()
        );
    }


    public Task<ProjectDetailsDto?> ReadAsync(int projectId)
    {
        var projects = from p in _context.Projects
            where p.Id == projectId
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
        return projects.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ProjectDto>> ReadAllAsync() =>
        (await _context.Projects
            .Select(p => new ProjectDto(p.Id, p.Author!.AzureAdToken, p.Author!.Name, p.Title, p.Description))
            .ToListAsync())
            .AsReadOnly();

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadTitleAsync(string input) {
        if (string.IsNullOrWhiteSpace(input)) {
            return new List<ProjectDetailsDto>();
        } else {
            var projects = from p in _context.Projects
                where p.Title.ToLower().Contains(input.ToLower())
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
            
            return await projects.ToListAsync();
        }
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadTitleGivenDegreeAsync(string searchString, Degree degree)
    {
        if (string.IsNullOrWhiteSpace(searchString)) {
            return new List<ProjectDetailsDto>();
        } else {
            var projects = from p in _context.Projects
                where p.Title.ToLower().Contains(searchString.ToLower()) && p.Degree == degree
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
            
            return await projects.ToListAsync();
        }
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAuthorAsync(string input) {
        if (string.IsNullOrWhiteSpace(input)) {
            return new List<ProjectDetailsDto>();
        } else {
            
            var projects = from p in _context.Projects
                where (p.Author!.Name.ToLower()).Contains(input.ToLower())
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
            
            return await projects.ToListAsync();
        }
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadAuthorGivenDegreeAsync(string searchString, Degree degree)
    {
        if (string.IsNullOrWhiteSpace(searchString)) {
            return new List<ProjectDetailsDto>();
        } else {
            
            var projects = from p in _context.Projects
                where (p.Author!.Name.ToLower()).Contains(searchString.ToLower()) && p.Degree == degree
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
            
            return await projects.ToListAsync();
        }
    }

    public async Task<Status> UpdateAsync(int id, ProjectUpdateDto project)
    {
        var entity = await _context.Projects
            .Include(p => p.Keywords)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (entity == null)
        {
            return Status.NotFound;
        }

        entity.Title = project.Title;
        entity.Author = await GetAuthorAsync(project.AuthorToken, project.AuthorName);
        entity.Degree = project.Degree;
        entity.Description = project.Description;
        entity.Ects = project.Ects;
        entity.FileUrl = project.FileUrl;
        entity.ImageUrl = project.ImageUrl;
        entity.LastUpdated = DateTime.UtcNow;
        entity.Keywords = await GetKeywordsAsync(project.Keywords).ToListAsync();
        
        await _context.SaveChangesAsync();
        return Status.Updated;
    }

    public async Task<Status> DeleteAsync(int projectId)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        if (project == null)
        {
            return Status.NotFound;
        }
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return Status.Deleted;
    }
    //----------Private helper methods---------------------------//
    //Get Author object from DTO Author string
    private async Task<Account?> GetAuthorAsync(string? azureAadToken, string? name) {
        if (name != null){
            return string.IsNullOrWhiteSpace(azureAadToken) ? null
                : await _context.Accounts.FirstOrDefaultAsync(a => a.AzureAdToken == azureAadToken) ??
                    new Account(azureAadToken, name);
        } else {
            return null;
        }
    }
    
    //Get collection of keyword objects from the keywords collection of strings in the DTOs
    private async IAsyncEnumerable<Keyword> GetKeywordsAsync(IEnumerable<string> keywords)
    {
        var existing = await _context.Keywords.Where(k => keywords.Contains(k.Word)).ToDictionaryAsync(k => k.Word);

        foreach (var keyword in keywords)
        {
            yield return existing.TryGetValue(keyword, out var p) ? p : new Keyword(keyword);
        }
    }
}

