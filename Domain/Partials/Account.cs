namespace BankApi.Domain.Entities
{
    public partial class Account
    {

        public override string ToString()
        {
            return $"{AccountId}, {AccountTypes.TypeName}, {Balance}";
        }

    }
}

