namespace ProjectBank.Infrastructure.Repository;

public class SearchManagement : ISearchManagement
{
    private readonly IProjectRepository _projectRepo;
    private readonly IKeywordRepository _keywordRepo;

    public SearchManagement(IProjectRepository projectRepo, IKeywordRepository keywordRepo)
    {
        _projectRepo = projectRepo;
        _keywordRepo = keywordRepo;
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString)
    {
        var projectsGivenTitle = await _projectRepo.ReadTitleAsync(searchString);
        var projectsGivenAuthor = await _projectRepo.ReadAuthorAsync(searchString);
        var projectsGivenKeyword = await _keywordRepo.ReadAllProjectsWithKeywordStringAsync(searchString);
    
        var projectComparer = new ProjectComparer();

        var projects = projectsGivenTitle.Union(projectsGivenAuthor, projectComparer).Union(projectsGivenKeyword, projectComparer);
        return projects.ToList();

    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString, Degree degree)
    {
        var projectsGivenTitle = await _projectRepo.ReadTitleGivenDegreeAsync(searchString, degree);
        var projectsGivenAuthor = await _projectRepo.ReadAuthorGivenDegreeAsync(searchString, degree);
        var projectsGivenKeyword = await _keywordRepo.ReadAllProjectsWithKeywordAndDegreeAsync(searchString, degree);
    
        var projectComparer = new ProjectComparer();

        var projects = projectsGivenTitle.Union(projectsGivenAuthor, projectComparer).Union(projectsGivenKeyword, projectComparer);
        return projects.ToList();
    }
}