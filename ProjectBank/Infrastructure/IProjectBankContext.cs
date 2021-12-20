using ProjectBank.Infrastructure.Entity;

namespace ProjectBank.Infrastructure;

public interface IProjectBankContext : IDisposable
{
    public DbSet<Account> Accounts { get; }
    public DbSet<Keyword> Keywords { get; }
    public DbSet<Project> Projects { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
