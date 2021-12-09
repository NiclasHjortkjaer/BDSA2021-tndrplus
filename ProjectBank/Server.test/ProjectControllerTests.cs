using Xunit;

namespace ProjectBank.Server.test;

public class ProjectControllerTests
{
    [Fact]
    public async Task Create_creates_project()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var toCreate = new ProjectCreateDto();
        var created = new ProjectDetailsDto(1, "AuthorToken", "AuthorName", "Title", "Description", Degree.Bachelor, "ImageUrl", "Body", 15, DateTime.UtcNow, new HashSet<string>());
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }  

    [Fact]
    public async Task Get_returns_projects_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var expected = Array.Empty<ProjectDto>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.ReadAllAsync()).ReturnsAsync(expected);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(ProjectDetailsDto));
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(42);

        // Assert
        Assert.Null(response);
    }

    [Fact]
    public async Task Get_given_existing_returns_project()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var repository = new Mock<IProjectRepository>();
        var project = new ProjectDetailsDto(1, "AuthorToken", "AuthorName", "Title", "Description", Degree.Bachelor, "ImageUrl", "Body", 15, DateTime.UtcNow, new HashSet<string>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(project);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(1);

        // Assert
        Assert.Equal(project, response);
    }

    [Fact]
    public async Task Put_given_unknown_id_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var project = new ProjectUpdateDto();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.UpdateAsync(1, project)).ReturnsAsync(NotFound);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Put(1, project);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_updates_project()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var project = new ProjectUpdateDto();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.UpdateAsync(1, project)).ReturnsAsync(Updated);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Put(1, project);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.DeleteAsync(42)).ReturnsAsync(Status.NotFound);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Delete(42);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_existing_returns_NoContent()
    {
        // Arrange
        var logger = new Mock<ILogger<ProjectController>>();
        var repository = new Mock<IProjectRepository>();
        repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Status.Deleted);
        var controller = new ProjectController(logger.Object, repository.Object);

        // Act
        var response = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }
}