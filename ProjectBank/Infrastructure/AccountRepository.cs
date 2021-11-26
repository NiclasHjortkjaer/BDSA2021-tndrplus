namespace ProjectBank.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private readonly IProjectBankContext _context;

    public AccountRepository(IProjectBankContext context)
    {
        _context = context;
    }

    public Task<AccountDto> CreateAsync(AccountCreateDto account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto> ReadAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<AccountDto>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(int id, AccountUpdateDto account)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int accountId)
    {
        throw new NotImplementedException();
    }
}

