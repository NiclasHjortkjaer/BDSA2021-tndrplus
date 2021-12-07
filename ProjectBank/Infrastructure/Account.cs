namespace ProjectBank.Infrastructure;
public class Account
{
    public Account(string azureAdToken, string firstName, string lastName)
    {
        AzureAdToken = azureAdToken;
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; set;}

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string AzureAdToken { get; set; }
    public ICollection<Project> AuthoredProjects { get; set; } = null!;

    public ICollection<Project> SavedProjects { get; set; } = null!;
}