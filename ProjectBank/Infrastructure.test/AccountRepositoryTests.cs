namespace ProjectBank.Infrastructure.test;

public class AccountRepositoryTests : IDisposable
{
    private readonly IProjectBankContext _context;
    
    private readonly IAccountRepository _repo;
    
    //detect redundant calls
    private bool _disposedValue;

    ~AccountRepositoryTests() => Dispose(false);
    public AccountRepositoryTests()
    {
        //establish connection
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ProjectBankContext>();
        builder.UseSqlite(connection);
        var context = new ProjectBankContext(builder.Options);
        context.Database.EnsureCreated();
        
        //seed some data
        var unknownAccount = new Account("UnknownToken", "Elon Musk") {Id = 1};
        var aiKeyword = new Keyword("AI") {Id = 1};
        var machineLearnKey = new Keyword("Machine Learning") {Id = 2};
        var saveListAccount = new Account("AuthorToken", "Billy Gates") {Id = 3};
        var aiProject = new Project("Artificial Intelligence 101")
        { 
            Id = 1, AuthorId = 1,Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey}, Degree = Degree.Bachelor,
            Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[] {saveListAccount}
        };
        var mlProject = new Project("Machine Learning for dummies")
        {
            Id = 2, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.Phd, LastUpdated = DateTime.UtcNow
        };
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2", "John Bezos") { Id = 2 });
        context.SaveChanges();
        
        //init dbContext and Repo
        _context = context;
        _repo = new AccountRepository(_context);
    }
    [Fact]
    public async Task CreateAsync_creates_new_Account_With_Generated_Id()
    {
        var created = new AccountCreateDto
        {
            AzureAAdToken = "Create",
            Name = "Larry John",
            SavedProjects = new HashSet<string> {"Ez OOP"}
        };
        var account = await _repo.CreateAsync(created);
        Assert.Equal(4, account.Id);
        Assert.Equal("Create", account.AzureAdToken);
        Assert.True(account.SavedProjects.SetEquals(new[] {"Ez OOP"}));
    }
    [Fact]
    public async Task ReadAllAsyncReturnsAllAccounts()
    {
        var accounts = await _repo.ReadAllAsync();
        Assert.Collection(accounts,
            account => Assert.Equal(new AccountDto(1, "UnknownToken", "Elon Musk"),account),
            account => Assert.Equal(new AccountDto(2, "Token2", "John Bezos"),account),
            account => Assert.Equal(new AccountDto(3, "AuthorToken", "Billy Gates"),account)
            );
    }
    [Fact]
    public async Task Read_Valid_Id_Returns_Account()
    {
        var account = await _repo.ReadAsync(3);
        
        Assert.Equal(3, account!.Id);
        Assert.Equal("AuthorToken", account.AzureAdToken);
        Assert.True(account.SavedProjects.SetEquals(new []{"Artificial Intelligence 101"}));
    }
    [Fact]
    public async Task Read_given_Valid_Token_Returns_Account()
    {
        var account = await _repo.ReadFromTokenAsync("AuthorToken");
        
        Assert.Equal(3, account!.Id);
        Assert.Equal("AuthorToken", account.AzureAdToken);
        Assert.True(account.SavedProjects.SetEquals(new []{"Artificial Intelligence 101"}));
    }
    
    [Fact]
    public async Task Read_Invalid_Id_Returns_Null()
    {
        var account = await _repo.ReadAsync(-1);
        Assert.Null(account);
    }
    [Fact]
    public async Task Read_Invalid_token_Returns_Null()
    {
        var account = await _repo.ReadFromTokenAsync("Invalid");
        Assert.Null(account);
    }
    [Fact]
    public async Task UpdateAsync_given_invalid_Account_returns_notFound()
    {
        var account = new AccountUpdateDto
        {
            Id = 111,
            AzureAAdToken = "UpdateToken",
            SavedProjects = new HashSet<string>()
        };
        var status = await _repo.UpdateAsync(111, account);
        Assert.Equal(Status.NotFound, status);
    }

    [Fact]
    public async Task UpdateAsync_given_valid_Id_Updates_account()
    {
        var account = new AccountUpdateDto
        {
            Id = 1,
            AzureAAdToken = "UpdatedToken",
            SavedProjects = new HashSet<string>()
        };
        var status = await _repo.UpdateAsync(1, account);
        Assert.Equal(Status.Updated, status);
        await _repo.ReadAsync(1);
    }
    
    [Fact]
    public async Task DeleteAsync_returns_notfound_given_invalid_Id()
    {
        var actual = await _repo.DeleteAsync(111);
        
        Assert.Equal(Status.NotFound, actual);
    }

    [Fact]
    public async Task DeleteAsync_returns_deleted_given_id()
    {
        var actual = await _repo.DeleteAsync(1);
        Assert.Equal(Status.Deleted, actual);
    }
    [Fact]
    public async Task DeleteAsync_Deletes_given_valid_id()
    {
        var status = await _repo.DeleteAsync(1);

        Assert.Equal(Status.Deleted, status);
        
        Assert.Null(await _context.Accounts.FindAsync(1));
    }
    
    [Fact]
    public async Task ReadLikedProjectsFromTokenAsync_return_saved_projects_from_azureToken()
    {
        var account = await _context.Accounts.FindAsync(3);
        var actualProject = await _context.Projects.FindAsync(1);
        
        Assert.True(account!.SavedProjects.Contains(actualProject!));
        
        var projects = await _repo.ReadLikedProjectsFromTokenAsync("AuthorToken");
        
        Assert.Equal(actualProject!.Id, projects.FirstOrDefault());
    }
    [Fact]
    public async Task ReadLikedProjectsFromTokenAsync_return_empty_on_wrong_token()
    {
       var projects = await _repo.ReadLikedProjectsFromTokenAsync("SupYall");
        
       Assert.True(projects.Count==0);
    }
    
    [Fact]
    public async Task AddLikedProjectAsync_adds_given_project_from_title_and_azureToken()
    {
        var account3 = await _context.Accounts.FindAsync(3);
        var actualProject = await _context.Projects.FindAsync(2);
        
        Assert.False(account3!.SavedProjects.Contains(actualProject ?? throw new InvalidOperationException()));
        
        var status = await _repo.AddLikedProjectAsync("AuthorToken", actualProject.Title);
        
        Assert.Equal(Status.Updated, status);
        
        Assert.True(account3.SavedProjects.Contains(actualProject));
    }

    [Fact]
    public async Task AddLikedProjectAsync_two_times_return_status_conflict()
    {
        var account3 = await _context.Accounts.FindAsync(3);
        var actualProject = await _context.Projects.FindAsync(2);

        Assert.False(account3!.SavedProjects.Contains(actualProject!));

        var status = await _repo.AddLikedProjectAsync("AuthorToken", actualProject?.Title!);
        var status2 = await _repo.AddLikedProjectAsync("AuthorToken", actualProject?.Title!);

        Assert.True(account3.SavedProjects.Contains(actualProject!));
        Assert.Equal(Status.Updated, status);
        Assert.Equal(Status.Conflict, status2);

    }

    [Fact]
    public async Task RemoveLikedProjectAsync_removes_given_project_from_title_and_azureToken()
    {
        var account3 = await _context.Accounts.FindAsync(3);
        var projectToRemove = await _context.Projects.FindAsync(1);    
        
        Assert.True(account3!.SavedProjects.Contains(projectToRemove!));
        
        var status = await _repo.RemoveLikedProjectAsync("AuthorToken", projectToRemove?.Title!);
        
        Assert.Equal(Status.Updated, status);
        
        Assert.False(account3.SavedProjects.Contains(projectToRemove!));
    }

    // Disposable methods.-----------------------------
    // Reference: https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing)
        {
            // dispose managed state (managed objects)
            _context.Dispose();
        }
        // free unmanaged resources (unmanaged objects) and override finalizer
        // set large fields to null
        _disposedValue = true;
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}