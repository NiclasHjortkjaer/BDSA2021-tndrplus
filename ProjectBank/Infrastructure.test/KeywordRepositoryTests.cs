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
        var unknownAccount = new Account("UnknownToken") {Id = 1};
        var aiKeyword = new Keyword("AI") {Id = 1};
        var machineLearnKey = new Keyword("Machine Learning") {Id = 2};
        var aiProject = new Project("Artificial Intelligence 101")
        {
            Id = 1, AuthorId = 1, Author = unknownAccount, Keywords = new[] {aiKeyword, machineLearnKey}, Ects = 7,
            Description = "A dummies guide to AI. Make your own AI friend today"
        };
        var saveListAccount = new Account("AuthorToken") {Id = 3, SavedProjects = new[] {aiProject}};
        context.Projects.Add(aiProject);
        context.Accounts.AddRange(saveListAccount, new Account("Token2") {Id = 2});
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.SaveChanges();

        //init dbContext and Repo
        _context = context;
        _repo = new KeywordRepository(_context);
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

    [Fact]
    public async Task CreateAsync_creates_new_keyword_with_generated_Id()
    {
        var keyword = await _repo.CreateAsync(new KeywordCreateDto("OOP"));
        var expectedId = 4;
        var actual = await _context.Keywords.FindAsync(expectedId);
        Assert.Equal(expectedId, actual.Id);
        Assert.Equal("OOP", actual.Word);
    }

    [Fact]
    public async Task CreateAsync_given_existing_keyword_returns_null()
    {
        var keyword = await _repo.CreateAsync(new KeywordCreateDto("AI"));
        Assert.Null(keyword);
    }
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
    [Fact]
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

        Assert.Equal(keyword.word, actual.Word);
    }

    [Fact]
    public async Task UpdateAsync_given_existing_word_returns_conflict()
    {
        var keyword = new KeywordUpdateDto(3, "AI");
        var status = await _repo.UpdateAsync(3, keyword);
        Assert.Equal(Status.Conflict, status);
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
