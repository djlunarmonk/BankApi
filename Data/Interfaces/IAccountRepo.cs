using BankApi.Domain.Entities;

namespace BankApi.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<Account?>> GetCustomerAccounts(int customerId, bool details = false);
        Task<Account> NewAccount(Account account);
        Task UpdateAccount();
    }
}
