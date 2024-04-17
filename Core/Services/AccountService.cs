using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.Entities;

namespace BankApi.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<Account> NewAccount(int customerId, bool standard = true)
        {
            try
            {
                var account = await _accountRepo.NewAccount(new Account(standard));
                if (account is null) return null;

                if (account.Dispositions is null) account.Dispositions = new List<Disposition>();

                account.Dispositions.Add(new Disposition(customerId, account.AccountId));
                await _accountRepo.UpdateAccount();

                return account;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }
}
