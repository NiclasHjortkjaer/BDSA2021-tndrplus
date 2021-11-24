namespace ProjectBank.Infrastructure;
public class Account
{
    public Account(string azureAdToken)
    {
        AzureAdToken = azureAdToken;
    }

    public int Id { get; set;}

    public string AzureAdToken { get; set; }

    public AccountType AccountType { get; set; }
    
    public ICollection<Project> SavedProjects { get; set; } = null!;
}