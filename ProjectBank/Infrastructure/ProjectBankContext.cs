namespace Infrastructure;

public class ProjectBankContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Project> Projects => Set<Project>();


}