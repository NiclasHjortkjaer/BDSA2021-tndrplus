using Microsoft.AspNetCore.Mvc;

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
    public async Task get_by_Token_returns_liked_project_ids() //heeer
    { 
        
        var azureAdToken = "AuthorToken2";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");

        Assert.NotNull(projectIDs);
        Assert.True(projectIDs.Count > 0);
        Assert.True(projectIDs.Contains(1));
        
    }
    
    [Fact]
    public async Task get_by_Token_returns_empty_list_on_no_liked()
    {
        
        var azureAdToken = "Token2";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.NotNull(projectIDs);
        Assert.True(projectIDs.Count == 0);
        
    }
    
    [Fact]
    public async Task get_by_Token_returns_empty_list_on_wrong_token()
    {
        
        var azureAdToken = "wrongToken";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.NotNull(projectIDs);
        Assert.Equal(projectIDs.Count, 0);
        
    }
    
    [Fact]
    public async Task Post_by_token_and_title_adds_to_accounts_list()
    {
        
        var azureAdToken = "UnknownToken";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.True(projectIDs.Count==0);
        
        var titleToAdd = "Artificial Intelligence 101";
        var response = await _client.PostAsJsonAsync($"api/Account/{azureAdToken}", titleToAdd);
        var actualResponse = await response.Content.ReadFromJsonAsync<Status>();
        
        var projectIDs2 = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.True(projectIDs2.Count != 0);
        Assert.True(projectIDs2.Contains(1));
        Assert.Equal(Status.Updated.ToString(),actualResponse.ToString());
        
    }
    
    [Fact]
    public async Task Post_by_token_and_title_return_not_found_on_wrong_token()
    {
        
        var azureAdToken = "wrongToken";
        
        var titleToAdd = "Artificial Intelligence 101";
        var response = await _client.PostAsJsonAsync($"api/Account/{azureAdToken}", titleToAdd);
        
        var actual = await response.Content.ReadFromJsonAsync<Status>();
        
        Assert.Equal(Status.NotFound.ToString(), actual.ToString());

    }
    [Fact]
    public async Task Post_by_token_and_title_return_conflict_on_redundant_project()
    {
        
        var azureAdToken = "AuthorToken";
        var titleToAdd = "Artificial Intelligence 101";
        
        await _client.PostAsJsonAsync($"api/Account/{azureAdToken}", titleToAdd);
        
        var response2 = await _client.PostAsJsonAsync($"api/Account/{azureAdToken}", titleToAdd);
        var actual = await response2.Content.ReadFromJsonAsync<Status>();
        
        Assert.Equal(Status.Conflict.ToString(), actual.ToString());
        
    }
    
    [Fact]
    public async Task Put_by_token_and_title_remove_project_from_accounts_list()
    {
        
        var azureAdToken = "AuthorToken";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.True(projectIDs.Contains(1));
        
        var titleToRemove = "Artificial Intelligence 101";
        
        var response = await _client.PutAsJsonAsync($"api/Account/{azureAdToken}/remove", titleToRemove);
        var actual = await response.Content.ReadFromJsonAsync<Status>();
        
        var projectIDs2 = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.Equal(Status.Updated.ToString(), actual.ToString());
        Assert.False(projectIDs2.Contains(1));
        
    }
    [Fact]
    public async Task Put_by_token_and_title_return_not_found_on_wrong_token()
    {
        
        var azureAdToken = "wrongToken";
        
        var titleToRemove = "Artificial Intelligence 101";
        
        var response = await _client.PutAsJsonAsync($"api/Account/{azureAdToken}/remove", titleToRemove);
        var actual = await response.Content.ReadFromJsonAsync<Status>();

        Assert.Equal(Status.NotFound.ToString(), actual.ToString());
        
    }

    [Fact]
    public async Task Put_by_token_and_title_return_conflict_on_not_liked_project()
    {
        
        var azureAdToken = "UnknownToken";
        var projectIDs = await _client.GetFromJsonAsync<List<int>>($"/api/Account/likedProduct/{azureAdToken}");
        
        Assert.False(projectIDs.Contains(1));
        
        var titleToRemove = "Artificial Intelligence 101";
        
        var response = await _client.PutAsJsonAsync($"api/Account/{azureAdToken}/remove", titleToRemove);
        var actual = await response.Content.ReadFromJsonAsync<Status>();

        Assert.Equal(Status.Conflict.ToString(), actual.ToString());
        
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

        Assert.Equal(new Uri("http://localhost/api/Account/getBy/5"), response.Headers.Location);

        var created = await response.Content.ReadFromJsonAsync<AccountDetailsDto>();
        
        Assert.NotNull(created);
        Assert.Equal("Jesper Buch", created.Name);
        Assert.Equal("PostAccount", created.AzureAdToken);
        Assert.Empty(created.SavedProjects);
        
    }
}