using BankApi.Domain.Entities;

namespace BankApi.Core.Interfaces
{
    public interface IAccountService
    {
        Task<Account> NewAccount(int customerId, bool standard = true);
    }
}
