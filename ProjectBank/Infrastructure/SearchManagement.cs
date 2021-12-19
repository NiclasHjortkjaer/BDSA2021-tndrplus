namespace ProjectBank.Infrastructure;

public class SearchManagement : ISearchManagement
{
    private readonly IProjectRepository _project_repo;
    private readonly IKeywordRepository _keyword_repo;

    public SearchManagement(IProjectRepository project_repo, IKeywordRepository keyword_repo)
    {
        _project_repo = project_repo;
        _keyword_repo = keyword_repo;
    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString)
    {
        var projectsGivenTitle = await _project_repo.ReadTitleAsync(searchString);
        var projectsGivenAuthor = await _project_repo.ReadAuthorAsync(searchString);
        var projectsGivenKeyword = await _keyword_repo.ReadAllProjectsWithKeywordStringAsync(searchString);
    
        var ProjectComparer = new ProjectComparer();

        var projects = projectsGivenTitle.Union(projectsGivenAuthor, ProjectComparer).Union(projectsGivenKeyword, ProjectComparer);
        return projects.ToList();

    }

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string searchString, Degree degree)
    {
        var projectsGivenTitle = await _project_repo.ReadTitleGivenDegreeAsync(searchString, degree);
        var projectsGivenAuthor = await _project_repo.ReadAuthorGivenDegreeAsync(searchString, degree);
        var projectsGivenKeyword = await _keyword_repo.ReadAllProjectsWithKeywordAndDegreeAsync(searchString, degree);
    
        var ProjectComparer = new ProjectComparer();

        var projects = projectsGivenTitle.Union(projectsGivenAuthor, ProjectComparer).Union(projectsGivenKeyword, ProjectComparer);
        return projects.ToList();
    }
}