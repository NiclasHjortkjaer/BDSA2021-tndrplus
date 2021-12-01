using Xunit;

namespace ProjectBank.Server.test;

public class KeywordControllerTests
{
    [Fact]
    public async Task Create_creates_keyword()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var toCreate = new KeywordCreateDto("Word");
        var created = new KeywordDto(1, "Word");
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtRouteResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.RouteName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }  

    [Fact]
    public async Task Get_returns_keyword_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var expected = Array.Empty<KeywordDto>();
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.ReadAllAsync()).ReturnsAsync(expected);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(KeywordDto));
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(42);

        // Assert
        Assert.Null(response);
    }

    [Fact]
    public async Task Get_given_existing_returns_keyword()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        var keyword = new KeywordDto(1, "Word");
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(keyword);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(1);

        // Assert
        Assert.Equal(keyword, response);
    }

    [Fact]
    public async Task Put_given_unknown_id_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var keyword = new KeywordUpdateDto(1, "Word");
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.UpdateAsync(1, keyword)).ReturnsAsync(NotFound);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Put(1, keyword);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_updates_keyword()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var keyword = new KeywordUpdateDto(1, "Word");
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.UpdateAsync(1, keyword)).ReturnsAsync(Updated);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Put(1, keyword);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.DeleteAsync(42)).ReturnsAsync(Status.NotFound);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Delete(42);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_existing_returns_NoContent()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Status.Deleted);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }
}