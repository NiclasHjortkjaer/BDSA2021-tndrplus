/*using ProjectBank.Client;

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
    public void FindWeightedRandomKeyword_returns_not_null()
    {
        finder.Setup(_client).GetAwaiter().GetResult();
        var keyword = finder.FindWeightedRandomKeyword();

        Assert.Equal("AI", keyword);
        Assert.True(keyword == "AI" || keyword == "Machine Learning" || keyword == "Design");
    }
}*/