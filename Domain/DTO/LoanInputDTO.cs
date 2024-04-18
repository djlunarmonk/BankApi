namespace BankApi.Domain.DTO
{
    public class LoanInputDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
    }
}
