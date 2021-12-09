namespace Server.Integration.Tests;

public class ProjectTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public ProjectTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact] 
    public async Task Get_returns_Project()
    {
        var projects = await _client.GetFromJsonAsync<ProjectDto[]>("/api/Project");
        Assert.NotNull(projects);
        Assert.True(projects.Length >= 2);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");
    }

    [Fact]
    public async Task Get_by_id_returns_project_given_valid_id()
    {
        var id = 1;
        var project = await _client.GetFromJsonAsync<ProjectDetailsDto>($"/api/Project/{id}");
        Assert.NotNull(project);
        Assert.Equal("Artificial Intelligence 101", project.Title);
        Assert.Equal("Elon Musk", project.AuthorName);
        Assert.NotEmpty(project.Keywords);

    }

    [Fact]
    public async Task Get_by_input_returns_projects_given_valid_title()
    {
        var title = "Artificial Intelligence 101";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]?>($"/api/Project/{title}");

        Assert.NotNull(projects);
        Assert.Equal(1, projects!.Length);

        var project = projects.FirstOrDefault();
        Assert.Equal("Artificial Intelligence 101", project!.Title);
        Assert.Equal("Elon Musk", project.AuthorName);
        Assert.NotEmpty(project.Keywords);
    }

    [Fact]
    public async Task Get_by_input_returns_projects_given_valid_author()
    {
        var author = "Elon";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]?>($"/api/Project/{author}");

        Assert.NotNull(projects);
        Assert.Equal(1, projects!.Length);

        var project = projects.FirstOrDefault();
        Assert.Equal("Artificial Intelligence 101", project!.Title);
        Assert.Equal("Elon Musk", project.AuthorName);
        Assert.NotEmpty(project.Keywords);
    }
}