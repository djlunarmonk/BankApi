using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace BankApi.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<AppUser> _manager;

        public LoginService(SignInManager<AppUser> manager)
        {
            _manager = manager;
        }

        public async Task<bool> Login(UserLoginDTO user)
        {
            var result = await _manager.PasswordSignInAsync(user.UserName, user.Password, false, false);

            if (result.Succeeded) { return true; }
            return false;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _manager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Please handle your errors in the future!" + ex.Message);
                return false;
            }
        }
    }
}
