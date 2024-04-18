namespace BankApi.Domain.DTO
{
    public class TransactionInputDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
