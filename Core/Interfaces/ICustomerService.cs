using BankApi.Domain.DTO;

namespace BankApi.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<string> CreateCustomer(CustomerNewDTO newCustomer);
        public Task<List<string>> GetAllCustomers();
    }
}
