using BankApi.Domain.DTO;

namespace BankApi.Core.Interfaces
{
    public interface ILoanService
    {
        Task<bool> GrantLoan(LoanInputDTO input);
    }
}
