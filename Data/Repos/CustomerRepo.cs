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

        public async Task<List<Customer>> GetCustomerByEmail(string email)
        {
            try
            {
                return await _context.Customers.Where(c => c.Emailaddress.ToLower() == email).ToListAsync();
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }
    }
}
