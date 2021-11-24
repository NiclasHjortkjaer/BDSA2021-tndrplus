namespace ProjectBank.Core;

public record AccountDto(
    int Id,
    string AzureAdToken,
    AccountType AccountType,
    ISet<ProjectDto> SavedProjects);

public record AccountCreateDto
{
    public string AzureAAdToken { get; init; }
    
    public AccountType AccountType { get; init; }
    
    public ISet<ProjectDto> SavedProjects { get; init; } = null!;

}

public record AccountUpdateDto : AccountCreateDto
{
    public int Id {get; init;}
}