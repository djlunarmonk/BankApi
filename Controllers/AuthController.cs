using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Authorize]
    [ApiController]
    public partial class AuthController : ControllerBase
    {
        private readonly ILoginService _service;

        public AuthController(ILoginService service)
        {

            _service = service;
        }


        [Route("Auth/Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (loginDTO == null) { return BadRequest("No input!"); }

            if (await _service.Login(loginDTO)) return Ok();
            else return Unauthorized("Can't login");
        }

        [Route("Auth/Logout")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (await _service.Logout()) return Ok("You are logged out!");
            return BadRequest("Logout error!");
        }
    }
}
