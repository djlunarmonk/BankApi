using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.DTO;
using BankApi.Domain.Entities;

namespace BankApi.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IAccountRepo _accountRepo;

        public LoanService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<bool> GrantLoan(LoanInputDTO input)
        {
            try
            {
                var account = await _accountRepo.GetAccountDetails(input.AccountId);
                if (account == null) { return false; }

                var loan = new Loan()
                {
                    AccountId = account.AccountId,
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                    Amount = input.Amount,
                    Duration = input.Duration,
                    Payments = 0,
                    Status = "Running"
                };
                account.Loans.Add(loan);
                await _accountRepo.UpdateAccount();

                account.Balance += input.Amount;
                account.Transactions.Add(new Transaction()
                {
                    AccountId = account.AccountId,
                    Date = loan.Date,
                    Type = "Credit",
                    Operation = $"Loan {loan.LoanId}",
                    Amount = input.Amount,
                    Balance = account.Balance
                });
                await _accountRepo.UpdateAccount();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
    }
}