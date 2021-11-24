namespace ProjectBank.Core;

public interface IAccountRepository
{
    Task<AccountDto> CreateAsync(AccountCreateDto account);
    Task<AccountDto> ReadAsync(int accountId);
    Task<IReadOnlyCollection<AccountDto>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, AccountUpdateDto account);
    Task<Status> DeleteAsync(int accountId);

}