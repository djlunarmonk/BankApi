using BankApi.Domain.DTO;
using System.Security.Claims;

namespace BankApi.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<string> CreateCustomer(CustomerNewDTO newCustomer);
        public Task<List<string>> GetAllCustomers();
        Task<int> GetCustomerId(ClaimsIdentity? claimsIdentity);
    }
}
