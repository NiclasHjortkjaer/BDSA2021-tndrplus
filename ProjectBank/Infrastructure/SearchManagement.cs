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

    public async Task<IReadOnlyCollection<ProjectDetailsDto>> ReadSearchQueryAsync(string input)
    {
        var projectsGivenTitle = await _project_repo.ReadTitleAsync(input);
        var projectsGivenAuthor = await _project_repo.ReadAuthorAsync(input);
        var projectsGivenKeyword = await _keyword_repo.ReadAllProjectsWithKeywordStringAsync(input);
    
        var ProjectComparer = new ProjectComparer();

        var projects = projectsGivenTitle.Union(projectsGivenAuthor, ProjectComparer).Union(projectsGivenKeyword, ProjectComparer);
        return projects.ToList();
    }
}