using BankApi.Data.Context;
using BankApi.Data.Interfaces;
using BankApi.Domain.Entities;

namespace BankApi.Data.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankAppDataContext _context;
        public AccountRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Account> NewAccount(Account account)
        {
            if (account is null) return null;
            // account.AccountTypes = await _context.AccountTypes.FindAsync(account.AccountTypesId);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task UpdateAccount()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
