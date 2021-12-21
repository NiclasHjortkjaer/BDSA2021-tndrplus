using Xunit;

namespace ProjectBank.Server.test;

public class KeywordControllerTests
{
    [Fact]
    public async Task Create_creates_keyword()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var toCreate = new KeywordCreateDto{ Word = "Word", Projects = new HashSet<string>()};
        var created = new KeywordDto(1, "Word");
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }  

    [Fact]
    public async Task Get_returns_keyword_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var expected = Array.Empty<KeywordDetailsDto>();
        var repository = new Mock<IKeywordRepository>();
        repository.Setup(m => m.ReadAllAsync()).ReturnsAsync(expected);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_on_string_given_existing_returns_keyword()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        //var keyword = new KeywordDto(1, "API");


        var keyList = new List<ProjectDetailsDto>()
        {
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
                new DateTime(50), new HashSet<string>() {"AI"})

        };

        repository.Setup(m => m.ReadAllProjectsWithKeywordStringAsync("AI")).ReturnsAsync(keyList);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("AI");

        // Assert
        Assert.Equal(keyList, response);
    }
    
    [Fact]
    public async Task Get_by_keyword_and_degree_returns_projects_of_those()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        //var keyword = new KeywordDto(1, "API");
        
        var keyList = new List<ProjectDetailsDto>()
        {
            new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
                new DateTime(50), new HashSet<string>() {"AI"})

        };

        repository.Setup(m => m.ReadAllProjectsWithKeywordAndDegreeAsync("AI", Degree.Bachelor)).ReturnsAsync(keyList);
        var controller = new KeywordController(logger.Object, repository.Object);


        // Act
        var response = await controller.Get("AI", Degree.Bachelor);

        // Assert
        Assert.Equal(keyList, response);
    }
    
    [Fact]
    public async Task GetKeywordStrings_returns_all_keywordStrings()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        var expected = Array.Empty<string>();
        repository.Setup(m => m.ReadAllWordsAsync()).ReturnsAsync(expected);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var actual = await controller.GetKeywordStrings();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetProjectGivenKeywordAndTimesSeenRand_returns()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        //var keyword = new KeywordDto(1, "API");


        var expected = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
                "A dummies guide to AI. Make your own AI friend today", Degree.Bachelor, null, null, 7.5f,
                new DateTime(50), new HashSet<string>() {"AI"});

        repository.Setup(m => m.ReadProjectGivenKeywordAndTimesSeenRandAsync("AI", 0, Degree.Bachelor)).ReturnsAsync(expected);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.GetBrowseAll("AI", 0, Degree.Bachelor);

        // Assert
        Assert.Equal(expected, response);
    }
    
    [Fact]
    public async Task GetProjectGivenKeywordAndTimesSeen_returns_on_degree()
    {
        // Arrange
        var logger = new Mock<ILogger<KeywordController>>();
        var repository = new Mock<IKeywordRepository>();
        //var keyword = new KeywordDto(1, "API");


        var expected = new ProjectDetailsDto(1, "UnknownToken", "Elon Musk", "Artificial Intelligence 101",
            "A dummies guide to AI. Make your own AI friend today", Degree.Master, null, null, 7.5f,
            new DateTime(50), new HashSet<string>() {"AI"});

        repository.Setup(m => m.ReadProjectGivenKeywordAndTimesSeenAsync("AI", 0, Degree.Master)).ReturnsAsync(expected);
        var controller = new KeywordController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get("AI", 0, Degree.Master);

        // Assert
        Assert.Equal(expected, response);
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