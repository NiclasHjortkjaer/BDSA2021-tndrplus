namespace ProjectBank.Infrastructure;
public class Account
{
    public int Id { get; set;}

    public string azureAdtoken { get; set; }

    public AccountType AccountType { get; set; }
    
    public ICollection<Project> SavedProjects { get; set; } = null!;
}