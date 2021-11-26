
namespace ProjectBank.Infrastructure.test;

public class AccountRepositoryTests
{
    private readonly IProjectBankContext _context;
    private readonly IAccountRepository _repo;
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
        var aiProject = new Project("Artificial Intelligence 101"){ Id = 1, AuthorId = 1,Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey}, Ects = 7, Description = "A dummies guide to AI. Make your own AI friend today"};
        var saveListAccount = new Account("AuthorToken") {Id = 3, SavedProjects = new[] {aiProject}, AccountType = AccountType.Student};
        context.Projects.Add(aiProject);
        context.Accounts.AddRange(saveListAccount, new Account("Token2") { Id = 2 , AccountType = AccountType.Supervisor});
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
}