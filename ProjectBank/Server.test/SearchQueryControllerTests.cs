namespace ProjectBank.Server.test;

public class SearchQueryControllerTests
{
    [Fact]
    public async Task Get_aiProject_given_Artificial()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        
        var management = new Mock<ISearchManagement>();

        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, "ImageUrl", "Body", 15, DateTime.UtcNow, new HashSet<string>());
        
        management.Setup(m => m.ReadSearchQueryAsync("Artificial")).ReturnsAsync(new []{aiProject});

        var controller = new SearchQueryController(logger.Object, management.Object);

        // Act
        var response = await controller.Get("Artificial");

        // Assert
        Assert.Equal(new []{aiProject}, response);
    }

    [Fact]
    public async Task Get_aiProject_given_Ar()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        var repository = new Mock<ISearchManagement>();
        var aiProject = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, "ImageUrl", "Body", 15, DateTime.UtcNow, new HashSet<string>());
        repository.Setup(m => m.ReadSearchQueryAsync("Ar")).ReturnsAsync(new []{aiProject});
        var controller = new SearchQueryController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("Ar");

        // Assert
        Assert.Equal(new []{aiProject}, response);
    }

    [Fact]
    public async Task Get_emptyArray_given_notExistingProject()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        var repository = new Mock<ISearchManagement>();
        repository.Setup(m => m.ReadSearchQueryAsync("asdf")).ReturnsAsync(new ProjectDetailsDto[]{});
        var controller = new SearchQueryController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("asdf");

        // Assert
        Assert.Equal(new ProjectDetailsDto[]{}, response);
    }

    [Fact]
    public async Task Get_mlProject_given_Billy()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        var repository = new Mock<ISearchManagement>();
        var mlProject = new ProjectDetailsDto(2, "AuthorToken1", "Billy Gates", "Machine Learning for dummies", "Very easy guide just for you", Degree.Phd, null, null, 15, DateTime.UtcNow, new HashSet<string>());
        repository.Setup(m => m.ReadSearchQueryAsync("Billy")).ReturnsAsync(new[]{mlProject});
        var controller = new SearchQueryController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("Billy");

        // Assert
        Assert.Equal(new[]{mlProject}, response);
    }

    [Fact]
    public async Task Get_aiProject_given_AI()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        var repository = new Mock<ISearchManagement>();

        var keyList = new List<ProjectDetailsDto>
        {
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
                new DateTime(50), new HashSet<string> {"AI"})

        };

        repository.Setup(m => m.ReadSearchQueryAsync("AI")).ReturnsAsync(keyList);
        var controller = new SearchQueryController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("AI");

        // Assert
        Assert.Equal(keyList, response);
    }

    [Fact]
    public async Task Get_aiProject_given_AI_and_Bachelor()
    {
        // Arrange
        var logger = new Mock<ILogger<SearchQueryController>>();
        var repository = new Mock<ISearchManagement>();

        var keyList = new List<ProjectDetailsDto>
        {
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
                new DateTime(50), new HashSet<string> {"AI"})

        };

        repository.Setup(m => m.ReadSearchQueryAsync("AI", Degree.Bachelor)).ReturnsAsync(keyList);
        var controller = new SearchQueryController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("AI", (int) Degree.Bachelor);

        // Assert
        Assert.Equal(keyList, response);
    }
}