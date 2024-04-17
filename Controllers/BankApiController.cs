using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [Authorize]
    [ApiController]
    public partial class BankApiController : ControllerBase
    {
        private readonly ILoginService _service;

        public BankApiController(ILoginService service)
        {

            _service = service;
        }

        [Route("/logout")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (await _service.Logout()) return Ok("You are logged out!");
            return BadRequest("Logout error!");
        }

        // Switched to using built-in Login of Identity Core as it handles cookie
        // with bearer token automagically

        [Route("User/login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (loginDTO == null) { return BadRequest(); }

            else return Ok(loginDTO);
        }

    }
}
