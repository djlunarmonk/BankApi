using BankApi.Data.Context;
using BankApi.Data.Interfaces;
using BankApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            account.AccountTypes = await _context.AccountTypes.FindAsync(account.AccountTypesId);

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

        public async Task<List<Account?>> GetCustomerAccounts(int customerId)
        {
            try
            {
                return await _context.Dispositions.Include(disp => disp.Account).ThenInclude(acc => acc.AccountTypes)
                                        .Where(disp => disp.CustomerId == customerId).Select(disp => disp.Account).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Account?> GetAccountDetails(int accountId)
        {
            return await _context.Accounts.Include(acc => acc.Transactions).Include(acc => acc.AccountTypes)
                                        .Include(acc => acc.Loans).FirstOrDefaultAsync(acc => acc.AccountId == accountId);
        }

        public async Task<bool> ValidateAccountOwner(int customerId, int accountId)
        {
            var disp = await _context.Dispositions.FirstOrDefaultAsync(disp => disp.CustomerId == customerId && disp.AccountId == accountId);
            if (disp is null) { return false; }
            return true;
        }
    }
}
