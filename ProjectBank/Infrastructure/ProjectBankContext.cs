namespace Infrastructure;

public class ProjectBankContext : DbContext
{
    public DbSet<Account> Accounts {get; set;}
    public DbSet<Keyword> Keywords {get; set;}
    public DbSet<Project> Projects {get; set;}


}