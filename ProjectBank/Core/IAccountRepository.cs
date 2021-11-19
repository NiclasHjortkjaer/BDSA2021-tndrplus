namespace Core;

public interface IAccountRepository
{
    Task<AccountDto> CreateAsync(AccountCreateDto account);
    Task<Option<AccountDto>> ReadAsync(int accountId);
    Task<IReadOnlyCollection<AccountDto>> ReadAllAsync();
    Task<Status> Update(int Id, AccountUpdateDto Account);
    Task<Status> Delete(int characterId);

}