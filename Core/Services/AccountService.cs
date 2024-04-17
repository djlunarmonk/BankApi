using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.Entities;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace BankApi.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(IAccountRepo accountRepo, UserManager<AppUser> userManager)
        {
            _accountRepo = accountRepo;
            _userManager = userManager;
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

        public async Task<List<string>> AccountOverview(int customerId = 1)
        {
            // var customerId = "Dicky";
            var accounts = await _accountRepo.GetCustomerAccounts(customerId);
            if (accounts is null) return null;
            var result = new List<string>();
            foreach (var account in accounts)
            {
                result.Add(account.ToString());
            }
            return result;
        }
    }
}
