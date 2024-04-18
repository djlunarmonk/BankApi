using BankApi.Core.Interfaces;
using BankApi.Domain.DTO;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace BankApi.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginService(SignInManager<AppUser> manager, UserManager<AppUser> userManager)
        {
            _signInManager = manager;
            _userManager = userManager;
        }

        public async Task<bool> Login(UserLoginDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);

            if (result.Succeeded) { return true; }
            return false;
        }

        //public async Task<SignInResult> LoginInAsync(UserLoginDTO model, bool rememberMe = true)
        //{
        //    var username = model.UserName;
        //    var password = model.Password;

        //    var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);

        //    if (result.Succeeded)
        //    {
        //        var user = await _userManager.FindByNameAsync(username);

        //        var claims = new[]
        //        {
        //        new Claim("customerId", user.CustomerId.ToString());
        //    };

        //    // Get the user's existing claims
        //    var existingClaims = await _userManager.GetClaimsAsync(user);

        //    // Add the new claim to the user's claims
        //    existingClaims.AddRange(claims);

        //    // Update the user's claims
        //    await _userManager.ReplaceClaimsAsync(user, existingClaims);
        //}

        //    return result;
        //}

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
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
