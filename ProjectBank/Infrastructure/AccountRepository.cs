namespace ProjectBank.Infrastructure;
public class AccountRepository : IAccountRepository
{
    private readonly IProjectBankContext _context;

    public AccountRepository(IProjectBankContext context)
    {
        _context = context;
    }

    public async Task<AccountDetailsDto> CreateAsync(AccountCreateDto account)

    { var conflict = await _context.Accounts
            .Where(k => k.AzureAdToken == account.AzureAAdToken)
            .Select(k => new AccountDto(k.Id, k.AzureAdToken))
            .FirstOrDefaultAsync();

        if (conflict != null)
        {
            return null!;
        }
        var newAccount = new Account(account.AzureAAdToken)

    {
        var newAccount = new Account(account.AzureAAdToken, account.Name)

        {
            SavedProjects = await GetSavedProjectsAsync(account.SavedProjects).ToListAsync()
        };
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return new AccountDetailsDto(
            newAccount.Id,
            newAccount.AzureAdToken,
            newAccount.Name
            newAccount.SavedProjects.Select(a => a.Title).ToHashSet());
    }

    public async Task<AccountDetailsDto> ReadAsync(int accountId)
    {
        var accounts = from a in _context.Accounts
            where a.Id == accountId
            select new AccountDetailsDto(
                a.Id,
                a.AzureAdToken,
                a.Name,
                a.SavedProjects.Select(p => p.Title).ToHashSet()
            );

        return await accounts.FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyCollection<AccountDto>> ReadAllAsync() =>
        (await _context.Accounts
            .Select(a => new AccountDto(a.Id, a.AzureAdToken, a.Name))
            .ToListAsync())
            .AsReadOnly();

    public async Task<Status> UpdateAsync(int id, AccountUpdateDto account)
    {
        var entitiy = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        if (entitiy == null)
        {
            return Status.NotFound;
        }
        
        entitiy.AzureAdToken = account.AzureAAdToken;
        entitiy.SavedProjects = await GetSavedProjectsAsync(account.SavedProjects).ToListAsync();
        
        await _context.SaveChangesAsync();
        return Status.Updated;
    }

    public async Task<Status> DeleteAsync(int accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null)
        {
            return Status.NotFound;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        
        return Status.Deleted;
    }
    //-----------------------Private helper methods---------------------------//
    private async IAsyncEnumerable<Project> GetSavedProjectsAsync(IEnumerable<string> projects)
    {
        var existing = await _context.Projects
            .Where(p => projects.Contains(p.Title))
            .ToDictionaryAsync(p => p.Title);

        foreach (var project in projects)
        {
            yield return existing.TryGetValue(project, out var p) ? p : new Project(project);
        }
    }
}

