using BankApi.Domain.Entities;

namespace BankApi.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<Account> NewAccount(Account account);
        Task UpdateAccount();
    }
}
