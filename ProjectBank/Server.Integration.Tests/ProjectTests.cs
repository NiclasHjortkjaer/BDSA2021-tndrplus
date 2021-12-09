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
    public async Task Post_returns_created()
    {
        var project = new ProjectCreateDto
        {
            Title = "Post Project",
            AuthorToken = "PostToken",
            AuthorName = "Jesper Buch",
            Description = "Project about making money",
            Ects = 10f,
            Degree = Degree.Master,
            Keywords = new HashSet<string>{"AI"}
        };

        var response = await _client.PostAsJsonAsync("/api/Project", project);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(new Uri("http://localhost/api/Project/3"), response.Headers.Location);

        var created = await response.Content.ReadFromJsonAsync<ProjectDetailsDto>();
        
        Assert.NotNull(created);
        Assert.Equal("PostToken", created.AuthorToken);
        Assert.Equal("Post Project", created.Title);
        Assert.Equal("Jesper Buch", created.AuthorName);
        Assert.Contains("AI", project.Keywords);
        Assert.Equal(10f, project.Ects);
        Assert.Equal("Project about making money", project.Description);
    }
}