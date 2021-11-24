namespace ProjectBank.Core;

public record ProjectDto(
    int Id,
    AccountDto? Author,
    string Type,
    string Title,
    IEnumerable<KeywordDto> Keyword,
    string Description,
    string Body,
    Degree RequiredDegree,
    float ECTS
);

public record ProjectCreateDto
{
    public AccountDto? Author { get; init; }
    
    [StringLength(50)]
    public string Type { get; init; }

    [StringLength(100)]
    public string Title { get; init; }
    
    public IEnumerable<KeywordDto> Keywords { get; init; }
    
    [StringLength(200)]
    public string Description { get; init; }

    [StringLength(500)]
    public string Body { get; init; }
    
    public Degree RequiredDegree { get; init; }
    
    public float ECTS {get; init;}


}

public record ProjectUpdateDto : ProjectCreateDto{
    public int Id {get; init;}
}

