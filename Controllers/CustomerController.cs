using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }

        [Route("Customer/AccountOverview")]
        [HttpGet]
        public async Task<IActionResult> AccountOverview()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int customerId = await _customerService.GetCustomerId(claimsIdentity);
            switch (customerId)
            {
                case 0:
                    return BadRequest("AppUser has no claims.");
                case -1:
                    return BadRequest("AppUser has no attached CustomerId");
                case 2:
                    return BadRequest("AppUser attached CustomerId is on the wrong form");
                default:
                    break;
            }
            var answer = await _accountService.AccountOverview(customerId);
            if (answer == null) { return BadRequest("Customer has no accounts"); }
            return Ok(answer);
        }

        [Route("Customer/Account/{accountId}")]
        [HttpGet]
        public async Task<IActionResult> GetAccountDetails(int accountId)
        {
            var answer = await _accountService.GetAccountDetails(accountId);
            if (answer == null) { return BadRequest("No such account."); }
            return Ok(answer);
        }

        [Route("Customer/Account/new")]
        [HttpGet]
        public async Task<IActionResult> NewAccount(bool standardAccount)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int customerId = await _customerService.GetCustomerId(claimsIdentity);
            switch (customerId)
            {
                case 0:
                    return BadRequest("AppUser has no claims.");
                case -1:
                    return BadRequest("AppUser has no attached CustomerId");
                case 2:
                    return BadRequest("AppUser attached CustomerId is on the wrong form");
                default:
                    break;
            }

            var answer = await _accountService.NewAccount(customerId, standardAccount);
            if (answer == null) { return BadRequest("Couldn't make new account at this time."); }
            return Ok(answer.ToString());
        }

        [Route("Customer/MakeTransaction")]
        [HttpPost]
        public async Task<IActionResult> MakeTransaction(TransactionInputDTO input)
        {
            if (input == null) return BadRequest("Invalid input");

            var claimsIdentity = User.Identity as ClaimsIdentity;
            int customerId = await _customerService.GetCustomerId(claimsIdentity);
            switch (customerId)
            {
                case 0:
                    return BadRequest("AppUser has no claims.");
                case -1:
                    return BadRequest("AppUser has no attached CustomerId");
                case 2:
                    return BadRequest("AppUser attached CustomerId is on the wrong form");
                default:
                    break;
            }

            var tranferValid = await _accountService.MakeTransaction(customerId, input);
            if (!tranferValid) { return BadRequest("Couldn't make transaction at this time. Are you're numbers right?"); }
            return Ok("Transaction made.");
        }

    }
}
