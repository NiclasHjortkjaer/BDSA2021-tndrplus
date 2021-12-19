namespace ProjectBank.Server.Controllers;
//from ondfisk BDSA 2021
[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ImageController : Controller
{
    private readonly IFileRepository _repository;

    private readonly IReadOnlyCollection<string> _allowedContentTypes = new[]
    {
        "image/gif",
        "image/jpeg",
        "image/jpg",
        "image/png"
    };

    public ImageController(IFileRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("{name}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(string name, [FromForm] IFormFile file)
    {
        if (!_allowedContentTypes.Contains(file.ContentType))
        {
            return BadRequest("Content type not allowed");
        }

        var (status, uri) = await _repository.CreateFileAsync(name, file.ContentType, file.OpenReadStream());

        return status == Status.Created
            ? new CreatedResult(uri, null)
            : status.ToActionResult();
    }
}
