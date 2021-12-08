namespace Server.Integration.Tests;

public class KeywordTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    
        public KeywordTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        
    [Fact] 
    public async Task Get_returns_Keywords()
    {
        var keywords = await _client.GetFromJsonAsync<KeywordDto[]>("/api/Keyword");
        Assert.NotNull(keywords);
        Assert.True(keywords.Length >= 2);
        Assert.Contains(keywords, k => k.Word == "AI");
    }

    [Fact]
    public async Task Get_with_Valid_id_returns_Keyword()
    {
        var id = 1;
        var keyword = await _client.GetFromJsonAsync<KeywordDto>($"/api/Keyword/getby/{id}");
        Assert.NotNull(keyword);
        Assert.Equal("AI", keyword.Word);
    }
    
    [Fact]
    public async Task get_projects_with_keyword_returns_projects()
    {
        var keyword = "AI";
        var projects = await _client.GetFromJsonAsync<ProjectDto[]>($"/api/Keyword/{keyword}");
        Assert.NotEmpty(projects);
        Assert.True(projects.Length >= 1);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.AuthorFirstName == "Elon");
        Assert.Contains(projects, p => p.AuthorLastName == "Musk");
        
    }
}
