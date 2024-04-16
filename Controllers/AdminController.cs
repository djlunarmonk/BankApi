using BankApi.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{

    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _service;

        public AdminController(ICustomerService service)
        {
            _service = service;
        }


        [Route("/admin/allcustomers")]
        [HttpGet]
        public async Task<IActionResult> NewCustomer()
        {
            var answer = await _service.GetAllCustomers();
            if (answer == null) { return BadRequest("Nope."); }
            return Ok(answer);
        }
    }
}
