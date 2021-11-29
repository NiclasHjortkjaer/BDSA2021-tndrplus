namespace ProjectBank.Infrastructure.test;

public class AccountRepositoryTests
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
        var unknownAccount = new Account("UnknownToken") {Id = 1, AccountType = AccountType.Student};
        var aiKeyword = new Keyword("AI") {Id = 1};
        var machineLearnKey = new Keyword("Machine Learning") {Id = 2};
        var saveListAccount = new Account("AuthorToken") {Id = 3,  AccountType = AccountType.Student};
        var aiProject = new Project("Artificial Intelligence 101")
        { 
            Id = 1, AuthorId = 1,Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey},
            Ects = 7, Description = "A dummies guide to AI. Make your own AI friend today", Created = DateTime.Now, Accounts = new[] {saveListAccount}
        };
        var mlProject = new Project("Machine Learning for dummies")
        {
            Id = 2, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.PHD, Created = DateTime.Now
            ,Keywords = new[] {machineLearnKey}
             
        };
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2") { Id = 2 , AccountType = AccountType.Supervisor});
        context.SaveChanges();
        
        //init dbContext and Repo
        _context = context;
        _repo = new AccountRepository(_context);
    }
    [Fact]
    public async Task ReadAllAsyncReturnsAllAccounts()
    {
        var accounts = await _repo.ReadAllAsync();
        Assert.Collection(accounts,
            account => Assert.Equal(new AccountDto(1, "UnknownToken", AccountType.Student),account),
            account => Assert.Equal(new AccountDto(2, "Token2", AccountType.Supervisor),account),
            account => Assert.Equal(new AccountDto(3, "AuthorToken", AccountType.Student),account)
            );
    }
    
    [Fact]
    public async Task Read_Valid_Id_Returns_Account()
    {
        var account = await _repo.ReadAsync(3);
        
        Assert.Equal(3, account.Id);
        Assert.Equal("AuthorToken", account.AzureAdToken);
        Assert.Equal(AccountType.Student, account.AccountType);
        Assert.True(account.SavedProjects.SetEquals(new []{"Artificial Intelligence 101"}));
    }

    [Fact]
    public async Task Read_Invalid_Id_Returns_Null()
    {
        var account = await _repo.ReadAsync(-1);
        Assert.Null(account);
    }

    [Fact]
    public async Task CreateAsync_creates_new_Account_With_Generated_Id()
    {
        var created = new AccountCreateDto
        {
            AzureAAdToken = "Create",
            AccountType = AccountType.Supervisor,
            SavedProjects = new HashSet<string> {"Ez OOP"}
        };
        var account = await _repo.CreateAsync(created);
        Assert.Equal(4, account.Id);
        Assert.Equal("Create", account.AzureAdToken);
        Assert.Equal(AccountType.Supervisor, account.AccountType);
        Assert.True(account.SavedProjects.SetEquals(new[] {"Ez OOP"}));
    }

    [Fact]
    public async Task DeleteAsync_returnes_notfound_given_invalid_Id()
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
    public async Task UpdateAsync_given_invalid_Account_returns_notFound()
    {
        var account = new AccountUpdateDto
        {
            Id = 111,
            AzureAAdToken = "UpdateToken",
            AccountType = AccountType.Supervisor,
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
            AccountType = AccountType.Student,
            SavedProjects = new HashSet<string>()
        };
        var status = await _repo.UpdateAsync(1, account);
        Assert.Equal(Status.Updated, status);
        var actual = await _repo.ReadAsync(1);

        Assert.Equal(account.AccountType, actual.AccountType);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }
            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }
    }

    //From BDSA2021.
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}