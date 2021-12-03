namespace ProjectBank.Core;

public record AccountDto(
    int Id,
    string AzureAdToken
);
    
public record AccountDetailsDto(
    int Id,
    string AzureAdToken,
    ISet<string> SavedProjects
);

public record AccountCreateDto
{
    public string AzureAAdToken { get; init; } = null!;
    public ISet<string> SavedProjects { get; init; } = new HashSet<string>();

}

public record AccountUpdateDto : AccountCreateDto
{
    public int Id {get; init;}
}