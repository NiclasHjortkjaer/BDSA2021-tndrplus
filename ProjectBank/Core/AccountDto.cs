namespace ProjectBank.Core;

public record AccountDto(
    int Id,
    string AzureAdToken,
    AccountType AccountType
    );
    
public record AccountDetailsDto(
    int Id,
    string AzureAdToken,
    AccountType AccountType,
    ISet<string>? SavedProjects
);

public record AccountCreateDto
{
    public string AzureAAdToken { get; init; } = null!;
    
    public AccountType AccountType { get; init; }
    
    public ISet<string>? SavedProjects { get; init; }

}

public record AccountUpdateDto : AccountCreateDto
{
    public int Id {get; init;}
}