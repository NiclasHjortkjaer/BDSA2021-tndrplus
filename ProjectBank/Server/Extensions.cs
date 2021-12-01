namespace ProjectBank.Server;
public static class Extensions
{
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

    public static ActionResult<T> ToActionResult<T>(this Option<T> option) where T : class
        => option.IsSome ? option.Value : new NotFoundResult();
}