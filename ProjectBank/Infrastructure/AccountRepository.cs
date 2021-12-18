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
        var conflict = await _context.Accounts
            .Where(a => a.AzureAdToken == account.AzureAAdToken)
            .Select(a => new AccountDto(a.Id, a.Name, a.AzureAdToken))
            .FirstOrDefaultAsync();

        if (conflict != null)
        {
            return null!;
        }
        
        var newAccount = new Account(account.AzureAAdToken, account.Name)
        {
            SavedProjects = await GetSavedProjectsAsync(account.SavedProjects).ToListAsync()
        };
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return new AccountDetailsDto(
            newAccount.Id,
            newAccount.AzureAdToken,
            newAccount.Name,
            newAccount.PictureUrl,
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
                a.PictureUrl,
                a.SavedProjects.Select(p => p.Title).ToHashSet()
            );

        return await accounts.FirstOrDefaultAsync();
    }
    
    public async Task<AccountDetailsDto> ReadFromTokenAsync(string azureAdToken)
    {
        var accounts = from a in _context.Accounts
            where a.AzureAdToken == azureAdToken
            select new AccountDetailsDto(
                a.Id,
                a.AzureAdToken,
                a.Name,
                a.PictureUrl,
                a.SavedProjects.Select(p => p.Title).ToHashSet()
            );

        return await accounts.FirstOrDefaultAsync();
    }
    public async Task<ICollection<int>> ReadLikedProjectsFromTokenAsync(string azureToken)
    {
        var account = await _context.Accounts.Include(a => a.SavedProjects).FirstOrDefaultAsync(a => a.AzureAdToken == azureToken);
        if (account == null)
        {
            return new List<int>();
        }
        
        var projects = account.SavedProjects;
        List<int> idsToReturn = new List<int>();
        if (projects != null)
        {
            foreach (var p in projects)
            {
                idsToReturn.Add(p.Id);
            }
        }

        return idsToReturn;
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
        entitiy.PictureUrl = account.PictureUrl;
        
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
    public async Task<Status> AddLikedProjectAsync(string azureToken, string projectTitle) //test den Carl
    {
        var account = await _context.Accounts.Include(a => a.SavedProjects).FirstOrDefaultAsync(a => a.AzureAdToken == azureToken);
        
        if (account == null)
        {
            return Status.NotFound;
        }

        var projectTitles = new List<string>();
        
        projectTitles = account.SavedProjects.Select(p => p.Title).ToList();
        if (projectTitles.Contains(projectTitle))
        {
            return Status.Conflict;
        }

        projectTitles.Add(projectTitle);
        
        account.SavedProjects = await GetSavedProjectsAsync(projectTitles).ToListAsync();
        
        await _context.SaveChangesAsync();
        return Status.Updated;
    }

    public async Task<Status> RemoveLikedProjectAsync(string azureToken, string projectTitle) //evt lav det her tilbage til id?? på projekt
    {
        var account = await _context.Accounts.Include(a => a.SavedProjects).FirstOrDefaultAsync(a => a.AzureAdToken == azureToken);
        
        if (account == null)
        {
            return Status.NotFound;
        }

        var projectTitles = new List<string>();

        if (account.SavedProjects != null)
        {
            projectTitles = account.SavedProjects.Select(p => p.Title).ToList();
            if (!projectTitles.Contains(projectTitle))
            {
                return Status.Conflict;
            }
            
            projectTitles.Remove(projectTitle);
        }
        
        account.SavedProjects = await GetSavedProjectsAsync(projectTitles).ToListAsync();
        
        //---
        var projectToRemove = await _context.Projects.Include(p => p.Accounts).FirstOrDefaultAsync(p => p.Title == projectTitle);
        
        if (projectToRemove == null)
        {
            return Status.Conflict;
        }
        //--- Because we work with a nested collection in a many to many relationship, we have to remove the account entry in the collection of the project we are removing from the account..
        //otherwise ef core cannot track the entry of same key value being removed, which will cause an exception, NotSupportedException---
        List<Account> accounts = projectToRemove.Accounts.ToList();

        accounts.Remove(account);
        projectToRemove.Accounts = accounts;

        await _context.SaveChangesAsync();
        return Status.Updated;
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

