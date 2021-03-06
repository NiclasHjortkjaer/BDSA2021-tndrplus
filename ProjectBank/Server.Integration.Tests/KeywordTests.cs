namespace Server.Integration.Tests;

public class KeywordTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public KeywordTests(CustomWebApplicationFactory factory)
        {
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
        Assert.True(keywords!.Length >= 2);
        Assert.Contains(keywords, k => k.Word == "AI");
        
    }

    [Fact]
    public async Task Get_with_Valid_id_returns_Keyword()
    {
        
        var id = 1;
        var keyword = await _client.GetFromJsonAsync<KeywordDto>($"/api/Keyword/getby/{id}");
        
        Assert.NotNull(keyword);
        Assert.Equal("AI", keyword!.Word);
        
    }
    
    [Fact]
    public async Task get_projects_with_keyword_returns_projects()
    {
        
        var keyword = "AI";
        var projects = await _client.GetFromJsonAsync<ProjectDto[]>($"/api/Keyword/{keyword}");
        
        Assert.NotEmpty(projects!);
        Assert.True(projects!.Length >= 1);
        Assert.Contains(projects, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects, p => p.AuthorName == "Elon Musk");

    }

    [Fact]
    public async Task Get_aiProject_given_AI_and_Bachelor()
    {
        
        var keyword = "AI";
        var degree = Degree.Bachelor;
        var projects = await _client.GetFromJsonAsync<ProjectDto[]>($"/api/Keyword/withType/{keyword}/{degree}");
        
        Assert.NotEmpty(projects!);
        Assert.Contains(projects!, p => p.Title == "Artificial Intelligence 101");
        Assert.Contains(projects!, p => p.AuthorName == "Elon Musk");
        
    }
    
    [Fact]
    public async Task get_projects_with_keyword_and_degree_returns_empty_on_wrong_degree()
    {
        
        var keyword = "AI";
        var degree = Degree.Master;
        var projects = await _client.GetFromJsonAsync<ProjectDto[]>($"/api/Keyword/withType/{keyword}/{degree}");
        
        Assert.Empty(projects!);
        
    }

    [Fact]
    public async Task Get_aiProject_given_AI_0_and_Bachelor()
    {
        
        var keyword = "Machine Learning";
        var timesSeen = 0;
        var degree = Degree.Bachelor;
        var project = await _client.GetFromJsonAsync<ProjectDetailsDto>($"/api/Keyword/typeOption/{keyword}/{timesSeen}/{degree}");
        
        Assert.NotNull(project);
        Assert.Equal("Artificial Intelligence 101", project!.Title);
        Assert.Equal("Elon Musk", project.AuthorName);
        Assert.NotEmpty(project.Keywords);
        
    }

    [Fact]
    public async Task GetKeywordStrings_returns_all_strings()
    {
        
        var words = await _client.GetFromJsonAsync<string[]>($"/api/Keyword/getStrings");
        
        Assert.Collection(words!,
            word => Assert.Equal("AI", word),
            word => Assert.Equal("Machine Learning", word),
            word => Assert.Equal("Design", word)
        );
        
    }
    
    [Fact]
    public async Task GetProjectGivenKeywordAndTimesSeen_returns_mlProject_given_AI_and_0_and_type()
    {
        
        var keyword = "AI";
        var timesSeen = 0;
        var degree = Degree.Phd;
        
        ProjectDetailsDto? actual = await _client.GetFromJsonAsync<ProjectDetailsDto>($"/api/Keyword/typeOption/{keyword}/{timesSeen}/{degree}");
        
        var mlProject = new ProjectDetailsDto(2, "UnknownToken", "Elon Musk", "Machine Learning for dummies", "Very easy guide just for you", Degree.Phd, null, null, 15, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"});

        Assert.Equal(2, actual!.Id);
        Assert.Equal(mlProject.AuthorToken, actual.AuthorToken);
        Assert.Equal(mlProject.AuthorName, actual.AuthorName);
        Assert.Equal(mlProject.Degree, actual.Degree);
        Assert.Equal(mlProject.Title, actual.Title);
        Assert.Equal(mlProject.Description, actual.Description);
        Assert.Equal(mlProject.ImageUrl, actual.ImageUrl);
        Assert.Equal(mlProject.FileUrl, actual.FileUrl);
        Assert.Equal(mlProject.LastUpdated, actual.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.Collection(actual.Keywords,
            word => Assert.Equal("AI", word),
            word => Assert.Equal("Machine Learning", word)
        );
        
    }
    
    [Fact]
    public async Task GetProjectGivenKeywordAndTimesSeenRand_returns_randomProject_given_AI_and_25_Bachelor()
    {
        
        var actual = await _client.GetFromJsonAsync<ProjectDetailsDto>($"/api/Keyword/AI/25/{Degree.Bachelor}");
        Assert.NotNull(actual);
        
    }
}
