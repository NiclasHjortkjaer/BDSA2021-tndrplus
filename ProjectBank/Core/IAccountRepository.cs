namespace ProjectBank.Core;

public interface IAccountRepository
{
    Task<AccountDetailsDto> CreateAsync(AccountCreateDto account);
    Task<AccountDetailsDto> ReadAsync(int accountId);
    Task<AccountDetailsDto> ReadFromTokenAsync(string azureAdToken);
    Task<IReadOnlyCollection<AccountDto>> ReadAllAsync();
    Task<Status> UpdateAsync(int id, AccountUpdateDto account);
    Task<Status> DeleteAsync(int accountId);

    Task<Status> AddLikedProjectAsync(string azureToken , string projectTitle);
    Task<Status> RemoveLikedProjectAsync(string azureToken, string projectTitle);
}