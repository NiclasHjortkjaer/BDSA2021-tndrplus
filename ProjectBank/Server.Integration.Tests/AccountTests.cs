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
        var account = await _client.GetFromJsonAsync<AccountDto>($"/api/Account/{id}");
        Assert.NotNull(account);
        Assert.Equal("Elon Musk",account.Name);
    }
}