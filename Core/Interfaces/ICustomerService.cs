namespace BankApi.Core.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<string>> GetAllCustomers();
    }
}
