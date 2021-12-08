namespace ProjectBank.Infrastructure.test;

public class KeywordRepositoryTests : IDisposable
{
    private readonly IProjectBankContext _context;
    private readonly IKeywordRepository _repo;

    private bool _disposedValue;

    ~KeywordRepositoryTests() => Dispose(false);

    public KeywordRepositoryTests()
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
            Id = 2, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.PHD, LastUpdated = DateTime.UtcNow
        };
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2", "John Bezos") { Id = 2 });
        context.SaveChanges();

        //init dbContext and Repo
        _context = context;
        _repo = new KeywordRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_creates_new_keyword_with_generated_Id()
    {
        var keyword = await _repo.CreateAsync(new KeywordCreateDto{Word = "OOP", Projects = new HashSet<string>()});
        var expectedId = 4;
        var actual = await _context.Keywords.FindAsync(expectedId);
        Assert.Equal(expectedId, actual!.Id);
        Assert.Equal(keyword.Word, actual.Word);
    }

    [Fact]
    public async Task CreateAsync_given_existing_keyword_returns_null()
    {
        var keyword = await _repo.CreateAsync(new KeywordCreateDto{Word = "AI"});
        Assert.Null(keyword);
    }
    
    [Fact]
    public async Task ReadAllAsync_Returns_All_Keywords()
    {
        var keywords = await _repo.ReadAllAsync();
        Assert.Collection(keywords,
            keyword => Assert.Equal(new KeywordDto(1, "AI"), keyword),
            keyword => Assert.Equal(new KeywordDto(2, "Machine Learning"), keyword),
            keyword => Assert.Equal(new KeywordDto(3, "Design"), keyword)
        );
    }

    [Fact]
    public async Task ReadAllProjectsWithKeywordAsync_returns_all_allprojects_with_keyword()
    {
        var projectsAi = await _repo.ReadAllProjectsWithKeywordAsync(new KeywordDto(4, "AI"));
        Assert.Collection(projectsAi,
            project => Assert.Equal(new ProjectDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today"), project)
        );
        var projectsMl = await _repo.ReadAllProjectsWithKeywordAsync(new KeywordDto(5, "Machine Learning"));
        Assert.Collection(projectsMl,
            project => Assert.Equal(new ProjectDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",

                "A dummies guide to AI. Make your own AI friend today"), project)
        );
    }
    
    [Fact]
    public async Task ReadAllProjectsWithKeywordStringAsync_returns_all_allprojects_with_keyword()
    {
        var projects = await _repo.ReadAllProjectsWithKeywordStringAsync("AI");
        Assert.Collection(projects,
            project => Assert.Equal(new ProjectDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today"), project)
        );
    }
    [Fact]
    public async Task ReadAllProjectsWithKeywordStringAsync_returns_empty_list_given_invalid_keyword()
    {
        var projects = await _repo.ReadAllProjectsWithKeywordStringAsync("Not AI");
        Assert.NotNull(projects);
        Assert.Empty(projects);
    }

    [Fact]
    public async Task ReadAsync_given_invalid_id_returns_null()
    {
        var keyword = await _repo.ReadAsync(111);
        Assert.Null(keyword);
    }

    [Fact]
    public async Task ReadAsync_given_valid_id_returns_keyword()
    {
        var keyword = await _repo.ReadAsync(1);
        var expected = new KeywordDto(1, "AI");
        
        Assert.Equal(expected, keyword);
    }

    /* [Fact]
    public async Task UpdateAsync_given_invalid_Keyword_returns_notFound()
    {
        var keyword = new KeywordUpdateDto(111, "ChangedWord");
        
        var status = await _repo.UpdateAsync(111, keyword);
        Assert.Equal(Status.NotFound, status);
    }

    [Fact]
    public async Task UpdateAsync_given_valid_Id_Updates_Keyword()
    {
        var keyword = new KeywordUpdateDto(1, "UpdatedWord");
        
        var status = await _repo.UpdateAsync(1, keyword);
        Assert.Equal(Status.Updated, status);
        
        var actual = await _repo.ReadAsync(1);

        Assert.Equal(keyword.Word, actual.Word);
    }

    [Fact]
    public async Task UpdateAsync_given_existing_word_returns_conflict()
    {
        var keyword = new KeywordUpdateDto(3, "AI");
        var status = await _repo.UpdateAsync(3, keyword);
        Assert.Equal(Status.Conflict, status);
    } */

    [Fact]
    public async Task DeleteAsync_returnes_notfound_given_invalid_Id()
    {
        var actual = await _repo.DeleteAsync(111);
        
        Assert.Equal(Status.NotFound, actual);
    }

    [Fact]
    public async Task DeleteAsync_returns_deleted_given_valid_id()
    {
        var actual = await _repo.DeleteAsync(3);
        Assert.Equal(Status.Deleted, actual);
    }
    [Fact]
    public async Task DeleteAsync_Deletes_given_valid_id()
    {
        var status = await _repo.DeleteAsync(3);

        Assert.Equal(Status.Deleted, status);
        
        Assert.Null(await _context.Keywords.FindAsync(3));
    }

    [Fact]
    public async Task DeleteAsync_returns_conflict_given_keyword_belogning_to_projects()
    {
        var status = await _repo.DeleteAsync(1);

        Assert.Equal(Status.Conflict, status);
        Assert.NotNull(await _context.Keywords.FindAsync(1));
    }
    
    //Disposable methods-------------------------------------
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
