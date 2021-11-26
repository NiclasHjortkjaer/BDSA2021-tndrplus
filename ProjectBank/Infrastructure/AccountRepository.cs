namespace ProjectBank.Infrastructure;
public class AccountRepository : IAccountRepository
{
    private readonly IProjectBankContext _context;

    public AccountRepository(IProjectBankContext context)
    {
        _context = context;
    }

    public async Task<AccountDetailsDto> CreateAsync(AccountCreateDto account)
    {
        var newAccount = new Account(account.AzureAAdToken)
        {
            AccountType = account.AccountType,
            SavedProjects = await GetSavedProjectsAsync(account.SavedProjects).ToListAsync();//Ved ikke hvad der er galt her
        };
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return new AccountDetailsDto(
            newAccount.Id,
            newAccount.AzureAdToken,
            newAccount.AccountType,
            newAccount.SavedProjects.Select(a => a.Title).ToHashSet());
    }

    public async Task<AccountDetailsDto> ReadAsync(int accountId)
    {
        var accounts = from a in _context.Accounts
            where a.Id == accountId
            select new AccountDetailsDto(
                a.Id,
                a.AzureAdToken,
                a.AccountType,
                a.SavedProjects.Select(p => p.Title).ToHashSet()
            );

        return await accounts.FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyCollection<AccountDto>> ReadAllAsync() =>
        (await _context.Accounts
            .Select(a => new AccountDto(a.Id, a.AzureAdToken, a.AccountType))
            .ToListAsync())
            .AsReadOnly();

    public Task<Status> UpdateAsync(int id, AccountUpdateDto account)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int accountId)
    {
        throw new NotImplementedException();
    }
    private async IAsyncEnumerable<Project> GetSavedProjectsAsync(IEnumerable<string> projects)
    {
        var existing = await _context.Projects.Where(p => projects.Contains(p.Title)).ToDictionaryAsync(p => p.Title);

        foreach (var project in projects)
        {
            yield return existing.TryGetValue(project, out var p) ? p : new Project(project);
        }
    }
}

