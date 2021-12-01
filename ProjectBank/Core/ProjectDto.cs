namespace ProjectBank.Core;

public record ProjectDto(
    int Id,
    string? Author,
    string Title,
    string? Description
);
public record ProjectDetailsDto(
    int Id,
    string? Author,
    string Title,
    string? Description,
    Degree? Degree,
    string? ImageUrl,
    string? Body,
    float? Ects,
    DateTime LastUpdated,
    ISet<string> Keywords
) : ProjectDto(Id, Author, Title, Description);


public record ProjectCreateDto
{
    public string? Author { get; init; }
    
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

    public DateTime LastUpdated { get; init; }


    public float? Ects { get; init; }

    public ISet<string> Keywords { get; init; } = null!;
    
}

public record ProjectUpdateDto : ProjectCreateDto{
    public int Id {get; init;}
}

