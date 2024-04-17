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
        private readonly ICustomerService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ICustomerService service, UserManager<AppUser> manager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _userManager = manager;
            _roleManager = roleManager;
        }


        [Route("/Admin/allcustomers")]
        [HttpGet]
        public async Task<IActionResult> AllCustomers()
        {
            var answer = await _service.GetAllCustomers();
            if (answer == null) { return BadRequest("Nope."); }


            return Ok(answer);
        }

        [Route("/Admin/NewCustomer")]
        [HttpPost]
        public async Task<IActionResult> NewCustomer(CustomerNewDTO newCustomer)
        {
            if (newCustomer == null) { return BadRequest("Invalid input."); }

            var answer = await _service.CreateCustomer(newCustomer);

            if (answer == null) { return BadRequest("Error in service."); }

            // Not great, maybe? Should be a better way, frontend will not be happy?
            return Ok(answer);
        }
    }
}
