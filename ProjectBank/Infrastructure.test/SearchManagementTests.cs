namespace ProjectBank.Infrastructure.test;

public class SearchManagementTests : IDisposable
{
    private readonly ISearchManagement _management;
    private readonly IProjectBankContext _context;
    
    //detect redundant calls
    private bool _disposedValue;

    ~SearchManagementTests() => Dispose(false);
    public SearchManagementTests()
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
            Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = new DateTime(50), Accounts = new[] {saveListAccount}
        };
        var mlProject = new Project("Machine Learning for dummies")
        {
            Id = 2, AuthorId = 1,Author = unknownAccount , Keywords = new[]{aiKeyword, machineLearnKey}, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.PHD, LastUpdated = DateTime.UtcNow
        };
        
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2", "John Bezos") { Id = 2 });
        context.SaveChanges();
        
        //init dbContext and Repo
        _context = context;
        _management = new SearchManagement(new ProjectRepository(_context), new KeywordRepository(_context));
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_listWithMlProject_given_Machine_Learning()
    {
        var projects = await _management.ReadSearchQueryAsync("Machine Learning for dummies");

        var mlProject = new ProjectDetailsDto(2, "UnknownToken", "Elon Musk", "Machine Learning for dummies", "Very easy guide just for you", Degree.PHD, null, null, 15, DateTime.UtcNow, new HashSet<string>());
        
        Assert.Single(projects);
        Assert.Equal(2, projects.First().Id);
        Assert.Equal(mlProject.AuthorToken, projects.First().AuthorToken);
        Assert.Equal(mlProject.AuthorName, projects.First().AuthorName);
        Assert.Equal(mlProject.Degree, projects.First().Degree);
        Assert.Equal(mlProject.Title, projects.First().Title);
        Assert.Equal(mlProject.Description, projects.First().Description);
        Assert.Equal(mlProject.ImageUrl, projects.First().ImageUrl);
        Assert.Equal(mlProject.FileUrl, projects.First().FileUrl);
        Assert.Equal(mlProject.LastUpdated, projects.First().LastUpdated, TimeSpan.FromSeconds(5));
        Assert.True(projects.First().Keywords.SetEquals(new[]{"AI", "Machine Learning"}));
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_emptylist_given_emptystring()
    {
        var projects = await _management.ReadSearchQueryAsync("");

        Assert.Empty(projects);
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_aiProject_and_mlProject_given_Elon_Musk() {
        var projects = await _management.ReadSearchQueryAsync("Elon Musk");

        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string> {"AI", "Machine Learning"});
        var mlProject = new ProjectDetailsDto(2, "UnknownToken", "Elon Musk", "Machine Learning for dummies", "Very easy guide just for you", Degree.PHD, null, null, 15, DateTime.UtcNow, new HashSet<string> {"AI", "Machine Learning"});

        Assert.Collection(projects,
                project => {
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    Assert.Equal(1, project.Id);
                    Assert.Equal(aiProject.AuthorToken, projects.First().AuthorToken);
                    Assert.Equal(aiProject.AuthorName, projects.First().AuthorName);
                    Assert.Equal(aiProject.Title, projects.First().Title);
                },
                project => {
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    Assert.Equal(2, project.Id);
                    Assert.Equal(mlProject.AuthorToken, projects.ElementAt(1).AuthorToken);
                    Assert.Equal(mlProject.AuthorName, projects.ElementAt(1).AuthorName);
                    Assert.Equal(mlProject.Title, projects.ElementAt(1).Title);
                }
        );
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_empty_list_given_empty_string() {
        var projects = await _management.ReadSearchQueryAsync(" ");

        Assert.Empty(projects);
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_aiProject_and_mlProject_given_ai()
    {
        var projects = await _management.ReadSearchQueryAsync("AI");
       
        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, DateTime.UtcNow, new HashSet<string> {"AI", "Machine Learning"});
        var mlProject = new ProjectDetailsDto(2, "UnknownToken", "Elon Musk", "Machine Learning for dummies", "Very easy guide just for you", Degree.PHD, null, null, 15, DateTime.UtcNow, new HashSet<string> {"AI", "Machine Learning"});

        Assert.Collection(projects,
                project => {
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    Assert.Equal(1, project.Id);
                    Assert.Equal(aiProject.AuthorToken, projects.First().AuthorToken);
                    Assert.Equal(aiProject.AuthorName, projects.First().AuthorName);
                    Assert.Equal(aiProject.Title, projects.First().Title);
                },
                project => {
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    Assert.Equal(2, project.Id);
                    Assert.Equal(mlProject.AuthorToken, projects.ElementAt(1).AuthorToken);
                    Assert.Equal(mlProject.AuthorName, projects.ElementAt(1).AuthorName);
                    Assert.Equal(mlProject.Title, projects.ElementAt(1).Title);
                }
        );
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_no_duplicates_given_Machine_Learning()
    {
        var projects = await _management.ReadSearchQueryAsync("Machine Learning");

        Assert.Equal(2, projects.Count());
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_empty_list_given_invalid_keyword()
    {
        var projects = await _management.ReadSearchQueryAsync("Not AI");
        Assert.NotNull(projects);
        Assert.Empty(projects);
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_list_with_aiProject_given_Machine_Learning_and_Bachelor()
    {
        var projects = await _management.ReadSearchQueryAsync("Machine Learning", Degree.Bachelor);

        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101", "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f, new DateTime(50), new HashSet<string> {"AI", "Machine Learning"});

        Assert.Single(projects);
        Assert.Equal(1, projects.First().Id);
        Assert.Equal(aiProject.AuthorToken, projects.First().AuthorToken);
        Assert.Equal(aiProject.AuthorName, projects.First().AuthorName);
        Assert.Equal(aiProject.Degree, projects.First().Degree);
        Assert.Equal(aiProject.Title, projects.First().Title);
        Assert.Equal(aiProject.Description, projects.First().Description);
        Assert.Equal(aiProject.ImageUrl, projects.First().ImageUrl);
        Assert.Equal(aiProject.FileUrl, projects.First().FileUrl);
        Assert.Equal(aiProject.LastUpdated, projects.First().LastUpdated, TimeSpan.FromSeconds(5));
        Assert.True(projects.First().Keywords.SetEquals(new[]{"AI", "Machine Learning"}));
    }

    [Fact]
    public async Task ReadSearchQueryAsync_returns_empty_list_given_Elon_Musk_and_Master()
    {
        var projects = await _management.ReadSearchQueryAsync("Elon Musk", Degree.Master);

        Assert.Empty(projects);
    }

    // Disposable methods.-----------------------------
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
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }    
}