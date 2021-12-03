using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace ProjectBank.Infrastructure.test;

public class ProjectRepositoryTests
{
    private readonly IProjectBankContext _context;
    
    private readonly IProjectRepository _repo;
    
    //detect redundant calls
    private bool _disposedValue;

    ~ProjectRepositoryTests() => Dispose(false);
    public ProjectRepositoryTests()
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
        var saveListAccount = new Account("AuthorToken") {Id = 3};
        var aiProject = new Project("Artificial Intelligence 101")
        { 
            Id = 1, AuthorId = 1,Author = unknownAccount ,Keywords = new[]{aiKeyword, machineLearnKey}, Degree = Degree.Bachelor,
            Ects = 7.5f, Description = "A dummies guide to AI. Make your own AI friend today", LastUpdated = DateTime.UtcNow, Accounts = new[] {saveListAccount}
        };
        var mlProject = new Project("Machine Learning for dummies")
        {
            Id = 2, Ects = 15, Description = "Very easy guide just for you", Degree = Degree.PHD, LastUpdated = DateTime.Now
        };
        context.Projects.AddRange(aiProject, mlProject);
        context.Keywords.Add(new Keyword("Design"){Id = 3});
        context.Accounts.Add( new Account("Token2") { Id = 2 });
        context.SaveChanges();
        
        //init dbContext and Repo
        _context = context;
        _repo = new ProjectRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_returns_new_project_with_generated_Id()
    {
        var project = new ProjectCreateDto
        {
            Title = "Big project",
            Author = "MeToken",
            Degree = Degree.Master,
            LastUpdated = DateTime.UtcNow,
            Ects = 30f,
            Keywords = new HashSet<string>(new []{"Keyword"}),
        };

        var created = await _repo.CreateAsync(project);
        
        Assert.Equal(3, created.Id);
        Assert.Equal(project.Title, created.Title);
        Assert.Equal(project.Description, created.Description);
        Assert.Equal(project.LastUpdated, created.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.Equal(project.Ects, created.Ects);
        Assert.True(created.Keywords.SetEquals(new []{"Keyword"}));
    }

    [Fact]
    public async Task ReadAllAsync_Returns_All_Keywords()
    {
        var projects = await _repo.ReadAllAsync();
        Assert.Collection(projects,
            project => Assert.Equal(new ProjectDto(1, "UnknownToken","Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today"), project),
            project => Assert.Equal(new ProjectDto(2,null, "Machine Learning for dummies",
                "Very easy guide just for you" ), project)
        );
    }


    [Fact]
    public async Task ReadAsync_returns_null_given_invalid_Id()
    {
        var project = await _repo.ReadAsync(111);
        Assert.Null(project);
    }

    [Fact]
    public async Task ReadAsync_returns_Project_given_valid_Id()
    {
        var project = await _repo.ReadAsync(1);
        var expected = new ProjectDetailsDto(1, "UnknownToken", "Artificial Intelligence 101",
            "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
            DateTime.UtcNow, new HashSet<string>(new[] {"AI", "Machine Learning"}));
        Assert.Equal(1, project.Id);
        Assert.Equal(expected.Author, project.Author);
        Assert.Equal(expected.Degree, project.Degree);
        Assert.Equal(expected.Title, project.Title);
        Assert.Equal(expected.Description, expected.Description);
        Assert.Equal(expected.ImageUrl, project.ImageUrl);
        Assert.Equal(expected.FileUrl, project.FileUrl);
        Assert.Equal(expected.LastUpdated, project.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.True(project.Keywords.SetEquals(new []{"AI", "Machine Learning"}));
    }


    [Fact]
    public async Task UpdateAsync_given_invalid_id_returns_notFound()
    {
        var project = new ProjectUpdateDto
        {
            Id = 111,
            Author = "UpdatedToken",
            FileUrl = "Im the new body",
            Degree = Degree.PHD,
            Description = "Very easy guide just for you",
            Title = "Machine Learning for dummies",
            Ects = 15
        };
        var status = await _repo.UpdateAsync(111,project);
        Assert.Equal(Status.NotFound, status);
    }

    [Fact]
    public async Task UpdateAsync_given_valid_updates_existing_project()
    {
        var project = new ProjectUpdateDto
        {
            Id = 2,
            Title = "Machine Learning for dummies",
            Author = "UpdatedToken",
            FileUrl = "Im the new body",
            Degree = Degree.PHD,
            Description = "Very easy guide just for you",
            Ects = 15,
            Keywords = new HashSet<string>()
        };
        var status = await _repo.UpdateAsync(2, project);
        Assert.Equal(Status.Updated, status);

        var updatedProject = await _repo.ReadAsync(2);
        Assert.Equal("Machine Learning for dummies", updatedProject.Title);
        Assert.Equal(DateTime.UtcNow, updatedProject.LastUpdated, TimeSpan.FromSeconds(5));
        Assert.Equal(project.Author, updatedProject.Author);
        Assert.Equal(project.FileUrl, updatedProject.FileUrl);
        Assert.Empty(updatedProject.Keywords);

    }

    [Fact]
    public async Task DeleteAsync_Returns_Not_Found_given_invalid_Id()
    {
        var status = await _repo.DeleteAsync(111);
        
        Assert.Equal(Status.NotFound, status);
    }
    [Fact]
    public async Task DeleteAsync_Returns_Deleted_given_valid_Id()
    {
        var status = await _repo.DeleteAsync(2);
        
        Assert.Equal(Status.Deleted, status);
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
