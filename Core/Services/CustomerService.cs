using AutoMapper;
using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.DTO;
using BankApi.Domain.Entities;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace BankApi.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerService(ICustomerRepo repo, IAccountService accountService, IMapper mapper,
                        UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _customerRepo = repo;
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<string>> GetAllCustomers()
        {
            var list = await _customerRepo.GetAllCustomers();
            var result = new List<string>();
            foreach (var c in list)
            {
                result.Add(_mapper.Map<CustomerDTO>(c).ToString());
            }
            return result;
        }

        public async Task<string> CreateCustomer(CustomerNewDTO newCustomer)
        {
            // Make sure emailadress is lowercase in case (Ha!) frontend did a poor job
            newCustomer.Emailaddress = newCustomer.Emailaddress.ToLower();
            // And did they attach a new password?
            if (string.IsNullOrEmpty(newCustomer.Password)) return "No password given.";

            if (newCustomer.UserName.Any(c => !char.IsAsciiLetterOrDigit(c)))
            { return "UserName must only contain letters and digits"; }

            // Is there already a customer with this email address?
            var duplicateCustomers = await _customerRepo.GetCustomerByEmail(newCustomer.Emailaddress);
            if (duplicateCustomers.Count != 0) return "Duplicate of Customer email in database.";

            // Or an AppUser in the Identity db?
            var duplicateAppUsers = await _userManager.FindByEmailAsync(newCustomer.Emailaddress);
            if (duplicateAppUsers != null) return "Duplicate of AppUser email in database.";
            duplicateAppUsers = await _userManager.FindByNameAsync(newCustomer.UserName);
            if (duplicateAppUsers != null) return "Duplicate of AppUser email in database.";

            // We insert a new Customer
            var createdCustomer = await _customerRepo.CreateCustomer(_mapper.Map<Customer>(newCustomer));
            if (createdCustomer is null) return "Error on creating new customer.";

            var answer = $"Customer created with new Id: {createdCustomer.CustomerId}. ";

            // Make a standard account for new user
            var account = await _accountService.NewAccount(createdCustomer.CustomerId);
            if (account is null) answer += "Error on creating account. ";
            else answer += $"Standard account created, no: {account.AccountId} ";

            // Let's make an AppUser
            var newUser = new AppUser()
            {
                UserName = newCustomer.UserName,
                Email = createdCustomer.Emailaddress,
                Customer = createdCustomer
            };

            var result = await _userManager.CreateAsync(newUser, newCustomer.Password);
            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync("Customer");
                if (!roleExists)
                {
                    var role = new IdentityRole();
                    role.Name = "Customer";
                    await _roleManager.CreateAsync(role);
                }
                var roleResult = await _userManager.AddToRoleAsync(newUser, "Customer");
                answer += roleResult.Succeeded ? "And added to AppUser Customers." :
                "And added to AppUsers, but couldn't be assign Customer status as of now";
                return answer;
            }
            return "Customer created, but cannot be added as AppUser right now.\n" + result.ToString();
        }

    }
}
