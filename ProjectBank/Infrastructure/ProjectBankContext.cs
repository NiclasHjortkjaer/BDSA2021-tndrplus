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
            .Entity<Project>()
            .Property(e => e.Degree)
            .HasMaxLength(100)
            .HasConversion(new EnumToStringConverter<Degree>());
        //insert unique attributes.
        modelBuilder
            .Entity<Keyword>()
            .HasIndex(k => k.Word)
            .IsUnique();
        modelBuilder
            .Entity<Account>()
            .HasIndex(k => k.AzureAdToken)
            .IsUnique();
        modelBuilder
            .Entity<Project>()
            .HasOne(p => p.Author)
            .WithMany(a => a.AuthoredProjects)
            .HasForeignKey(p => p.AuthorId);
        modelBuilder
            .Entity<Account>()
            .HasMany(a => a.SavedProjects)
            .WithMany(p => p.Accounts);
    }

}