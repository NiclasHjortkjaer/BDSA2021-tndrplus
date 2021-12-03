namespace ProjectBank.Infrastructure;
public class Account
{
    public Account(string azureAdToken)
    {
        AzureAdToken = azureAdToken;
    }

    public int Id { get; set;}

    public string AzureAdToken { get; set; }
    public ICollection<Project> AuthoredProjects { get; set; } = null!;

    public ICollection<Project> SavedProjects { get; set; } = null!;
}