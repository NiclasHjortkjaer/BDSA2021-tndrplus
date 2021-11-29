using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjectBank.Infrastructure;

public class ProjectBankContext : DbContext, IProjectBankContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Project> Projects => Set<Project>();

    public ProjectBankContext(DbContextOptions<ProjectBankContext> options) : base(options){
        
     }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Account>()
            .Property(e => e.AccountType)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<AccountType>());
        modelBuilder
            .Entity<Project>()
            .Property(e => e.Degree)
            .HasMaxLength(100)
            .HasConversion(new EnumToStringConverter<Degree>());
        //insert unique attributes.
        modelBuilder
            .Entity<Keyword>()
            .HasIndex(k => k.Word)
            .IsUnique();
    }

}