[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PdfController : Controller
{
    private readonly IPdfRepository _repository;

    private readonly IReadOnlyCollection<string> _allowedContentTypes = new[]
    {
        "pdf/pdf",
        
    };

    public PdfController(IPdfRepository repository)
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

        var (status, uri) = await _repository.CreatePdfAsync(name, file.ContentType, file.OpenReadStream());

        return status == Status.Created
            ? new CreatedResult(uri, null)
            : status.ToActionResult();
    }
}