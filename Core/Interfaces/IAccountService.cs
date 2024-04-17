using BankApi.Domain.Entities;

namespace BankApi.Core.Interfaces
{
    public interface IAccountService
    {
        Task<List<string>> AccountOverview(int userId);
        Task<Account> NewAccount(int customerId, bool standard = true);
    }
}
