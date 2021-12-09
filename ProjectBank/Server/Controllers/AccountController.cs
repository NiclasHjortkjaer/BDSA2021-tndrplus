using ProjectBank.Server.Extensions;

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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<AccountDto>> Get()
        => await _repository.ReadAllAsync();

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(AccountDto), 200)]
    public async Task<AccountDetailsDto>? Get(int id)
       => await _repository.ReadAsync(id);
    
    [AllowAnonymous]
    [HttpGet("{}")] //herfra
    [ProducesResponseType(typeof(Status),404)]
    [ProducesResponseType(typeof(Status),200)]
    public async Task<Status>? Get(int accountId, int projectId)
        => await _repository.AddLikedProjectAsync(accountId,projectId);

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), 201)]
    public async Task<IActionResult> Post(AccountCreateDto account)
    {
        var created = await _repository.CreateAsync(account);

        return CreatedAtRoute(nameof(Get), new { created.Id }, created);
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Put(int id, [FromBody] AccountUpdateDto account)
        => (await _repository.UpdateAsync(id, account)).ToActionResult();

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}