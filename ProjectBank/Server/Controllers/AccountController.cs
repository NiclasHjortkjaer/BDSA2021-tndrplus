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
    public Task<IReadOnlyCollection<AccountDto>> Get()
        => _repository.ReadAllAsync();

    [AllowAnonymous]
    [HttpGet("getBy/{id}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(AccountDto), 200)]
    public Task<AccountDetailsDto>? Get(int id)
       => _repository.ReadAsync(id);
    
    [AllowAnonymous]
    [HttpGet("{azureAdToken}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(AccountDto), 200)]
    public Task<AccountDetailsDto> Get(string azureAdToken)
        => _repository.ReadFromTokenAsync(azureAdToken);
    
    [Authorize]
    [HttpGet("likedProduct/{azureToken}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ICollection<int>), 200)]
    public Task<ICollection<int>>? GetLiked(string azureToken)
        => _repository.ReadLikedProjectsFromTokenAsync(azureToken);


    [Authorize]
    [HttpPost("{azureToken}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Post(string azureToken, [FromBody] string projectTitle)
    {
        var response = await _repository.AddLikedProjectAsync(azureToken,projectTitle);
        return CreatedAtAction(nameof(Get), response);
    }

    [Authorize]
    [HttpPut("{azureToken}/remove")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put(string azureToken, [FromBody] string projectTitle)
    {
        var response = await _repository.RemoveLikedProjectAsync(azureToken,projectTitle);
        return CreatedAtAction(nameof(Get), response);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), 201)]
    public async Task<IActionResult> Post(AccountCreateDto account)
    {
        var created = await _repository.CreateAsync(account);
        return CreatedAtAction(nameof(Get), new { created.Id }, created);
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