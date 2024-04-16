using BankApi.Data.Context;
using BankApi.Data.Interfaces;
using BankApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private BankAppDataContext _context;

        public CustomerRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.Take(50).ToListAsync();
        }
    }
}
