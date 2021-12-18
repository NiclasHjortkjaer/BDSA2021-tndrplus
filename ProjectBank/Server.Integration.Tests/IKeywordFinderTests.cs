using ProjectBank.Client;

namespace Server.Integration.Tests;

public class IKeywordFinderTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private IKeywordFinder finder = new KeywordFinder();
    
    public IKeywordFinderTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task FindWeightedRandomKeyword_returns_not_null()
    {
        await finder.Setup(_client);
        var keyword = finder.FindWeightedRandomKeyword();

        Assert.True(keyword == "AI" || keyword == "Machine Learning" || keyword == "Design");
    }

    [Fact]
    public async Task UpdateRatioAsync_returns_NotFound_given_Hardware()
    {
        await finder.Setup(_client);
        var status = finder.UpdateRatioAsync("Hardware", true);

        Assert.Equal(Status.NotFound, status);
    }

    [Fact]
    public async Task UpdateRatioAsync_returns_Updated_given_AI()
    {
        await finder.Setup(_client);
        var status = finder.UpdateRatioAsync("AI", true);

        Assert.Equal(Status.Updated, status);
    }

    [Fact]
    public async Task UpdateRatioAsync_sets_aiRatio_20_given_AI_true()
    {
        await finder.Setup(_client);
        var status = finder.UpdateRatioAsync("AI", true);

        var aiRatio = finder.Ratios["AI"];

        Assert.Equal(Status.Updated, status);
        Assert.Equal(20, aiRatio);
    }

    [Fact]
    public async Task UpdateRatioAsync_sets_mlRatio_5_given_Machine_Learning_false()
    {
        await finder.Setup(_client);
        var status = finder.UpdateRatioAsync("Machine Learning", false);

        var mlRatio = finder.Ratios["Machine Learning"];

        Assert.Equal(Status.Updated, status);
        Assert.Equal(5, mlRatio);
    }

    [Fact]
    public async Task ReadProjectGivenKeywordAsync_returns_aiProject_given_ai()
    {
        await finder.Setup(_client);
        var actual = await finder.ReadProjectGivenKeywordAsync("AI");

        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"});

        Assert.Equal(1, actual.Id);
        Assert.Equal(aiProject.AuthorToken, actual.AuthorToken);
        Assert.Equal(aiProject.AuthorName, actual.AuthorName);
        Assert.Equal(aiProject.Degree, actual.Degree);
        Assert.Equal(aiProject.Title, actual.Title);
        Assert.Equal(aiProject.Description, actual.Description);
        Assert.Equal(aiProject.ImageUrl, actual.ImageUrl);
        Assert.Equal(aiProject.FileUrl, actual.FileUrl);
        Assert.Equal(aiProject.LastUpdated, actual.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.True(actual.Keywords.SetEquals(new string[]{"AI", "Machine Learning"}));
    }

    [Fact]
    public async Task ReadProjectGivenKeywordAsync_returns_mlProject_given_Machine_Learning_and_called_twice()
    {
        await finder.Setup(_client);
        await finder.ReadProjectGivenKeywordAsync("Machine Learning");

        var actual = await finder.ReadProjectGivenKeywordAsync("Machine Learning");

        var mlProject = new ProjectDetailsDto(2, "UnknownToken", "Elon Musk", "Machine Learning for dummies", "Very easy guide just for you", Degree.PHD, null, null, 15, DateTime.UtcNow, new HashSet<string>(){"AI", "Machine Learning"});

        Assert.Equal(2, actual.Id);
        Assert.Equal(mlProject.AuthorToken, actual.AuthorToken);
        Assert.Equal(mlProject.AuthorName, actual.AuthorName);
        Assert.Equal(mlProject.Degree, actual.Degree);
        Assert.Equal(mlProject.Title, actual.Title);
        Assert.Equal(mlProject.Description, actual.Description);
        Assert.Equal(mlProject.ImageUrl, actual.ImageUrl);
        Assert.Equal(mlProject.FileUrl, actual.FileUrl);
        Assert.Equal(mlProject.LastUpdated, actual.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.True(actual.Keywords.SetEquals(new string[]{"AI", "Machine Learning"}));
    }
}