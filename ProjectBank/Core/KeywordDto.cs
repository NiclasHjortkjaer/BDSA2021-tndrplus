namespace Core;

public record KeywordDto(
    int Id,
    string Word,
    string Project
);
public record KeywordCreateDto
{
    [StringLength(50)]
    public string Word { get; init; }
    
    [StringLength(100)]
    public string Project { get; init; }
}
