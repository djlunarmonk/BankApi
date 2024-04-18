namespace BankApi.Domain.Entities
{
    public partial class Transaction
    {
        public override string? ToString()
        {
            return $"{TransactionId} {AccountId} {Date} {Type} {Operation} {Amount} {Balance} {AccountNavigation.AccountId}";
        }
    }
}
