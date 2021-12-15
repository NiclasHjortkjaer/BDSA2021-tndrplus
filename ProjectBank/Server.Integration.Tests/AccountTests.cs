namespace Server.Integration.Tests;

public class AccountTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public AccountTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Fact] 
    public async Task Get_returns_Accounts()
    {
        var accounts = await _client.GetFromJsonAsync<AccountDto[]>("/api/Account");
        Assert.NotNull(accounts);
        Assert.True(accounts.Length >= 2);
        Assert.Contains(accounts, a => a.Name == "Elon Musk");
    }

    [Fact]
    public async Task get_by_id_returns_account_by_id()
    {
        var id = 1;
        var account = await _client.GetFromJsonAsync<AccountDto>($"/api/Account/getBy/{id}");
        Assert.NotNull(account);
        Assert.Equal("Elon Musk",account.Name);
    }
    [Fact]
    public async Task get_by_Token_returns_account()
    {
        var azureAdToken = "UnknownToken";
        var account = await _client.GetFromJsonAsync<AccountDto>($"/api/Account/{azureAdToken}");
        Assert.NotNull(account);
        Assert.Equal("Elon Musk",account.Name);
    }

    [Fact]
    public async Task Post_returns_created()
    {
        var account = new AccountCreateDto
        {
            AzureAAdToken = "PostAccount",
            Name = "Jesper Buch",
            SavedProjects = new HashSet<string>()
        };

        var response = await _client.PostAsJsonAsync("/api/Account", account);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(new Uri("http://localhost/api/Account/getBy/4"), response.Headers.Location);

        var created = await response.Content.ReadFromJsonAsync<AccountDetailsDto>();
        
        Assert.NotNull(created);
        Assert.Equal("Jesper Buch", created.Name);
        Assert.Equal("PostAccount", created.AzureAdToken);
        Assert.Empty(created.SavedProjects);
    }
}