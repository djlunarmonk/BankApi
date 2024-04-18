using BankApi.Domain.Entities;

namespace BankApi.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<Account?> GetAccountDetails(int accountId);
        Task<List<Account?>> GetCustomerAccounts(int customerId);
        Task<Account> NewAccount(Account account);
        Task UpdateAccount();
        Task<bool> ValidateAccountOwner(int customerId, int accountId);
    }
}
