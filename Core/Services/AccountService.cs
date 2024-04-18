using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.DTO;
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

        public async Task<List<string>> AccountOverview(int customerId)
        {

            var accounts = await _accountRepo.GetCustomerAccounts(customerId);
            if (accounts is null) return null;
            var result = new List<string>();
            foreach (var account in accounts)
            {
                result.Add(account.ToString());
            }
            return result;
        }

        public async Task<List<string>> GetAccountDetails(int accountId)
        {
            var account = await _accountRepo.GetAccountDetails(accountId);
            if (account is null) return null;
            var result = new List<string>() { account.ToString() };
            foreach (var trans in account.Transactions.OrderByDescending(t => t.Date).ToList())
            {
                result.Add(trans.ToString());
            }
            return result;
        }


        public async Task<bool> MakeTransaction(int customerId, TransactionInputDTO input)
        {
            try
            {
                var validation = await _accountRepo.ValidateAccountOwner(customerId, input.FromAccountId);
                if (!validation) return false;

                var fromAccount = await _accountRepo.GetAccountDetails(input.FromAccountId);
                if (fromAccount is null) return false;
                if (fromAccount.Balance < input.Amount) return false;

                var toAccount = await _accountRepo.GetAccountDetails(input.ToAccountId);
                if (toAccount is null) return false;

                fromAccount.Balance -= input.Amount;
                fromAccount.Transactions.Add(new Transaction()
                {
                    AccountId = fromAccount.AccountId,
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                    Type = "Debit",
                    Operation = $"tranfer to account {toAccount.AccountId}",
                    Amount = 0 - input.Amount,
                    Balance = fromAccount.Balance
                });

                toAccount.Balance += input.Amount;
                toAccount.Transactions.Add(new Transaction()
                {
                    AccountId = toAccount.AccountId,
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                    Type = "Credit",
                    Operation = $"tranfer from account {fromAccount.AccountId}",
                    Amount = input.Amount,
                    Balance = toAccount.Balance
                });
                await _accountRepo.UpdateAccount();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
