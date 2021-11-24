namespace ProjectBank.Core;

public record ProjectDto(
    int Id,
    AccountDto? Author,
    string Title,
    string? Description
);
public record ProjectDetailsDto(
    int Id,
    AccountDto? Author,
    Degree Degree,
    string Title,
    string? Description,
    string? ImageUrl,
    string? Body,
    float? Ects,
    ISet<KeywordDto> Keywords
);


public record ProjectCreateDto
{
    public AccountDto? Author { get; init; }
    
    public Degree? Degree { get; init; }
    
    [StringLength(100)]
    public string Title { get; init; }

    [StringLength(500)]
    public string? Description { get; init; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; init; }

    [StringLength(10000)]
    public string? Body { get; init; }

    
    public float? Ects { get; init; }
    
    public ISet<KeywordDto> Keywords { get; init; } = null!;
    
}

public record ProjectUpdateDto : ProjectCreateDto{
    public int Id {get; init;}
}

