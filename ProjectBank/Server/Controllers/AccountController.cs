namespace ProjectBank.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class AccountController : ControllerBase 
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountRepository _repository;

    public AccountController(ILogger<AccountController> logger, IAccountRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
}