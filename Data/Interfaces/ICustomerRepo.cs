using BankApi.Domain.Entities;

namespace BankApi.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAllCustomers();
    }
}
