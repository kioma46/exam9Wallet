namespace exam9Wallet.BLL.Interfaces;

public interface IServiceAccountService
{
    Task CreateDefaultAccountsForUserAsync(int userId);
    Task SeedAccountsForAllExistingUsersAsync();
}