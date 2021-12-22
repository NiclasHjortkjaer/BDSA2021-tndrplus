namespace ProjectBank.Server.Extensions;

public static class Extensions
{
    //Extension method for IActionResult return type used for multiple ActionResults
    //Taken from Rasmus Lystrøm's BDSA2021 Repository on GitHub: https://github.com/ondfisk/BDSA2021/tree/main/MyApp.Server/Model
    public static IActionResult ToActionResult(this Status status) 
            => status switch
            {
                Updated => new NoContentResult(),
                Deleted => new NoContentResult(),
                NotFound => new NotFoundResult(),
                Conflict => new ConflictResult(),
                BadRequest => new BadRequestResult(),
                _ => throw new NotSupportedException($"{status} not supported")
            };
}