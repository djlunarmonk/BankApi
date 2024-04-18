using BankApi.Domain.DTO;
using BankApi.Domain.Entities;

namespace BankApi.Core.Interfaces
{
    public interface IAccountService
    {
        Task<List<string>> AccountOverview(int userId);
        Task<List<string>> GetAccountDetails(int accountId);
        Task<bool> MakeTransaction(int customerId, TransactionInputDTO input);
        Task<Account> NewAccount(int customerId, bool standard = true);
    }
}
