namespace ProjectBank.Core;

public record ProjectDto(
    int Id,
    string? AuthorToken,
    string? AuthorName,
    string Title,
    string? Description
);
public record ProjectDetailsDto(
    int Id,
    string? AuthorToken,
    string? AuthorName,
    string Title,
    string? Description,
    Degree? Degree,
    string? ImageUrl,
    string? FileUrl,
    float? Ects,
    DateTime LastUpdated,
    ISet<string> Keywords
) : ProjectDto(Id, AuthorToken, AuthorName, Title, Description);


public record ProjectCreateDto
{
    public string? AuthorToken { get; init; } = null!;
    
    [StringLength(50)]
    public string? AuthorName { get; init; } = null!;

    [StringLength(100)]
    public string Title { get; init; } = null!;
    
    [StringLength(500)]
    public string? Description { get; init; }
    public Degree? Degree { get; init; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; init; }

    [StringLength(250)]
    [Url]
    public string? FileUrl { get; init; }

    public DateTime LastUpdated { get; init; }
    
    public float? Ects { get; init; }

    public ISet<string> Keywords { get; init; } = new HashSet<string>();
    
}

public record ProjectUpdateDto : ProjectCreateDto{
    public int Id {get; init;}
}

