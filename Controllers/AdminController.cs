using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILoanService _loanService;
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ICustomerService customerService, ILoanService loanservice, IAccountService accountService,
                                UserManager<AppUser> manager, RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _loanService = loanservice;
            _customerService = customerService;
            _userManager = manager;
            _roleManager = roleManager;
        }


        [Route("/Admin/AllCustomers")]
        [HttpGet]
        public async Task<IActionResult> AllCustomers()
        {
            var answer = await _customerService.GetAllCustomers();
            if (answer == null) { return BadRequest("Nope."); }


            return Ok(answer);
        }

        [Route("/Admin/NewCustomer")]
        [HttpPost]
        public async Task<IActionResult> NewCustomer(CustomerNewDTO newCustomer)
        {
            if (newCustomer == null) { return BadRequest("Invalid input."); }

            var answer = await _customerService.CreateCustomer(newCustomer);

            if (answer == null) { return BadRequest("Error in customerService."); }

            // Not great, maybe? Should be a better way, frontend will not be happy?
            return Ok(answer);
        }

        [Route("/Admin/NewLoan")]
        [HttpPost]
        public async Task<IActionResult> NewLoan(LoanInputDTO input)
        {
            if (input == null) { return BadRequest("Invalid input."); }

            var answer = await _loanService.GrantLoan(input);

            if (answer == false) { return BadRequest("Error in loanService."); }

            // Not great, maybe? Should be a better way, frontend will not be happy?
            return Ok("Loan granted and issued!");
        }

        [Route("Admin/Account/{accountId}")]
        [HttpGet]
        public async Task<IActionResult> GetAccountDetails(int accountId)
        {
            var answer = await _accountService.GetAccountDetails(accountId);
            if (answer == null) { return BadRequest("No such account."); }
            return Ok(answer);
        }
    }
}
