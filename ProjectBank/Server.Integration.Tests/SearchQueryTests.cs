namespace Server.Integration.Tests;

public class SearchQueryTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public SearchQueryTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact] 
    public async Task Get_Artificial_Intelligence_101_returns_aiProject()
    {
        var title = "Artificial Intelligence 101";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{title}");
        Assert.NotNull(projects);
        Assert.True(projects.Length == 1);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");
    }

    [Fact] 
    public async Task Get_AI_returns_aiProject()
    {
        var keyword = "AI";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{keyword}");
        Assert.NotNull(projects);
        Assert.True(projects.Length == 2);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.Title == "Machine Learning for dummies");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");
    }

    [Fact] 
    public async Task Get_Elon_Musk_returns_aiProject()
    {
        var author = "Elon Musk";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{author}");
        Assert.NotNull(projects);
        Assert.True(projects.Length == 2);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.Title == "Machine Learning for dummies");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");
    }

    [Fact] 
    public async Task Get_emptyString_returns_empty_array()
    {
        var title = "";
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{title}");
        Assert.NotNull(projects);
        Assert.True(projects.Length == 0);
    }

    [Fact]
    public async Task Get_aiProject_given_AI_and_Bachelor()
    {
        var keyword = "AI";
        var degree = Degree.Bachelor;
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{keyword}/{degree}");
        Assert.NotNull(projects);
        Assert.True(projects.Length == 1);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");
    }

    [Fact]
    public async Task Get_empty_list_given_AI_and_Master()
    {
        var keyword = "AI";
        var degree = Degree.Master;
        var projects = await _client.GetFromJsonAsync<ProjectDetailsDto[]>($"/api/SearchQuery/{keyword}/{degree}");
        Assert.Empty(projects);
    }
}