using Xunit;

namespace ProjectBank.Server.test;

public class AccountControllerTests
{
    [Fact]
    public async Task Create_creates_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var toCreate = new AccountCreateDto();
        var created = new AccountDto(1, "AzureAdToken", AccountType.Supervisor, new HashSet<ProjectDto>());
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new AccountController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtRouteResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.RouteName);
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
        var controller = new AccountController(logger.Object, repository.Object);

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
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(AccountDto));
        var controller = new AccountController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(42);

        // Assert
        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_returns_account()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var repository = new Mock<IAccountRepository>();
        var account = new AccountDto(1, "AzureAdToken", AccountType.Supervisor, new HashSet<ProjectDto>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(account);
        var controller = new AccountController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(1);

        // Assert
        Assert.Equal(account, response.Value);
    }

    [Fact]
    public async Task Put_given_unknown_id_returns_NotFound()
    {
        // Arrange
        var logger = new Mock<ILogger<AccountController>>();
        var account = new AccountUpdateDto();
        var repository = new Mock<IAccountRepository>();
        repository.Setup(m => m.UpdateAsync(1, account)).ReturnsAsync(NotFound);
        var controller = new AccountController(logger.Object, repository.Object);

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
        var controller = new AccountController(logger.Object, repository.Object);

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
        var controller = new AccountController(logger.Object, repository.Object);

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
        var controller = new AccountController(logger.Object, repository.Object);

        // Act
        var response = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(response);
    }
}