using BankApi.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var answer = await _accountService.AccountOverview(1);
            if (answer == null) { return BadRequest(); }
            return Ok(answer);
        }
    }
}
