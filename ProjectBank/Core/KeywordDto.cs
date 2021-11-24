namespace ProjectBank.Core;

public record KeywordDto(
    int Id,
    string Word);
public record KeywordDetailsDto(
    int Id,
    string Word,
    ISet<ProjectDto> Projects);
public record KeywordCreateDto
{
    [StringLength(50)]
    public string Word { get; init; }

    private ISet<ProjectDto> Projects { get; init; } = null!;
}

public record KeywordUpdateDto : KeywordCreateDto {
    public int Id { get; init; }
}