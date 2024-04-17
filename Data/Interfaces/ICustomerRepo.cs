using BankApi.Domain.Entities;

namespace BankApi.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<List<Customer>> GetCustomerByEmail(string email);
        Task<Customer> GetCustomerById(int id);
    }
}
