namespace ProjectBank.Server.Controllers;
//Used for generating and and seeding an HttpClient
//Heavily inspired by Rasmus Lystrøm's BDSA2021 repository on GitHub: https://github.com/ondfisk/BDSA2021/tree/main/MyApp.Server
[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PdfController : Controller
{
    private readonly IFileRepository _repository;

    private readonly IReadOnlyCollection<string> _allowedContentTypes = new[]
    {
        "application/pdf",
    };

    public PdfController(IFileRepository repository)
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