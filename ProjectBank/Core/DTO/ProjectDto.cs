using ProjectBank.Core.Enum;

namespace ProjectBank.Core.DTO;

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
    public string? AuthorToken { get; set; } = null!;
    
    [StringLength(50)]
    public string? AuthorName { get; set; } = null!;

    [StringLength(100)]
    public string Title { get; set; } = null!;
    
    [StringLength(500)]
    public string? Description { get; set; }
    public Degree? Degree { get; set; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; set; }

    [StringLength(250)]
    [Url]
    public string? FileUrl { get; set; }

    public DateTime LastUpdated { get; set; }
    
    public float? Ects { get; set; }

    public ISet<string> Keywords { get; set; } = new HashSet<string>();
    
}

public record ProjectUpdateDto : ProjectCreateDto{
    public int Id {get; set;}
}

