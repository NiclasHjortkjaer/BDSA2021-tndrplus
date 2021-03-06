namespace ProjectBank.Server.test;

public class AccountControllerTests
{
    [Fact]
    public async Task Create_creates_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var toCreate = new AccountCreateDto();
        var created = new AccountDetailsDto(1, "AzureAdToken", "Warren Buffet",null ,new HashSet<string>());
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new AccountController(repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    } 
    

    [Fact]
    public async Task Get_returns_accounts_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var expected = Array.Empty<AccountDto>();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.ReadAllAsync()).ReturnsAsync(expected);
        var controller = new AccountController(repository.Object);

        // Act
        var actual = await controller.Get();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(AccountDetailsDto));
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Get(42)!;

        // Assert
        Assert.Null(response);
    }
    
    [Fact]
    public async Task Get_given_non_existing_Token_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.ReadFromTokenAsync("Invalid")).ReturnsAsync(default(AccountDetailsDto));
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Get("Invalid");

        // Assert
        Assert.Null(response);
    }
    [Fact]
    public async Task Get_LikedProjects_given_azureToken_returns_collection_of_projectDetailDTOs()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        ICollection<int> expected = new List<int>(){1};
        repository.Setup(m => m.ReadLikedProjectsFromTokenAsync("token")).ReturnsAsync(expected);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.GetLiked("token")!;

        // Assert
        Assert.Equal(response, expected);
        //Assert.Null(response);
    }
    [Fact]
    public async Task Put_project_add_on_azureToken_and_projectTitle()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();

        repository.Setup(m => m.AddLikedProjectAsync("ya", "AI")).ReturnsAsync(Status.Created);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Post("ya","AI");
        
        // Assert
        Assert.IsType<CreatedAtActionResult>(response);
    }
    
    [Fact]
    public async Task Put_project_remove_on_azureToken_and_projectTitle()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();

        repository.Setup(m => m.RemoveLikedProjectAsync("ya","AI")).ReturnsAsync(Status.Deleted);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Put("ya","AI");

        // Assert
        Assert.IsType<CreatedAtActionResult>(response);
    }

    [Fact]
    public async Task Get_given_existing_returns_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        var account = new AccountDetailsDto(1, "AzureAdToken", "Warren Buffet",null, new HashSet<string>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(account);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Get(1)!;

        // Assert
        Assert.Equal(account, response);
    }
    public async Task Get_given_existing_token_returns_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        var account = new AccountDetailsDto(1, "AzureAdToken", "Warren Buffet",null, new HashSet<string>());
        repository.Setup(m => m.ReadFromTokenAsync("AzureAdToken")).ReturnsAsync(account);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Get("AzureAdToken");

        // Assert
        Assert.Equal(account, response);
    }

    [Fact]
    public async Task Put_given_unknown_id_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var account = new AccountUpdateDto();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.UpdateAsync(1, account)).ReturnsAsync(NotFound);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Put(1, account);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_updates_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var account = new AccountUpdateDto();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.UpdateAsync(1, account)).ReturnsAsync(Updated);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Put(1, account);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_given_non_existing_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.DeleteAsync(42)).ReturnsAsync(Status.NotFound);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Delete(42);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_existing_returns_NoContent()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Status.Deleted);
        var controller = new AccountController(repository.Object);

        // Act
        var response = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }
}