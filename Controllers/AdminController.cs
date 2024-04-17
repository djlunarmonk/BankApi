using BankApi.Core.Interfaces;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{

    [ApiController]
    [Authorize]
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


        [Route("/admin/allcustomers")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> NewCustomer()
        {
            var answer = await _service.GetAllCustomers();
            if (answer == null) { return BadRequest("Nope."); }

            //bool x = await _roleManager.RoleExistsAsync("Admin");
            //if (!x)
            //{
            //    // first we create Admin role   
            //    var role = new IdentityRole();
            //    role.Name = "Admin";
            //    await _roleManager.CreateAsync(role);
            //}
            ////Then we create a user 
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //if (user != null)
            //{
            //    var result = await _userManager.AddToRoleAsync(user, "Admin");
            //}


            //var userRole = User.FindFirstValue(ClaimTypes.Role);
            //if (userRole != null) await Console.Out.WriteAsync(userRole.ToString());
            return Ok(answer);
        }
    }
}
