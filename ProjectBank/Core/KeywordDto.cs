namespace ProjectBank.Core;

public record KeywordDto(
    int Id,
    string Word
);

public record KeywordDetailsDto(
    int Id,
    string Word,
    ISet<string> Projects) : KeywordDto(Id, Word);

public record KeywordCreateDto
{
    [Required, StringLength(50)] public string Word = null!;
    public ISet<string> Projects = new HashSet<string>();
}

public record KeywordUpdateDto : KeywordCreateDto
{
    public int Id { get; init; }
} 

