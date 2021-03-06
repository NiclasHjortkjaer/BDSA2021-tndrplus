namespace ProjectBank.Infrastructure.Entity;
public class Account
{
    public Account(string azureAdToken, string name)
    {
        AzureAdToken = azureAdToken;
        Name = name;
    }

    public int Id { get; set;}

    [StringLength(50)]

   
    public string Name { get; set; }

    public string AzureAdToken { get; set; }
    
    public string? PictureUrl { get; set; }
    public ICollection<Project> AuthoredProjects { get; set; } = null!;

    public ICollection<Project> SavedProjects { get; set; } = null!;
}