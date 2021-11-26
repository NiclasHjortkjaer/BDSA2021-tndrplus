namespace ProjectBank.Core;

public interface IAccountRepository
{
    Task<AccountDetailsDto> CreateAsync(AccountCreateDto account);
    Task<AccountDetailsDto> ReadAsync(int accountId);
    Task<IReadOnlyCollection<AccountDto>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, AccountUpdateDto account);
    Task<Status> DeleteAsync(int accountId);

}