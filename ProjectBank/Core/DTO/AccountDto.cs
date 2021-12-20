namespace ProjectBank.Core.DTO;

public record AccountDto(
    int Id,
    string AzureAdToken,
    string Name
);
    
public record AccountDetailsDto(
    int Id,
    string AzureAdToken,
    string Name,
    string? PictureUrl,
    ISet<string> SavedProjects
);

public record AccountCreateDto
{
    public string AzureAAdToken { get; init; } = null!;

    [StringLength(50)]
    public string Name { get; init; } = null!;

    public string? PictureUrl { get; set; }

    public ISet<string> SavedProjects { get; init; } = new HashSet<string>();

}

public record AccountUpdateDto : AccountCreateDto
{
    public int Id {get; init;}
}