using BankApi.Domain.DTO;

namespace BankApi.Core.Interfaces
{
    public interface ILoginService
    {
        Task<bool> Login(UserLoginDTO user);

        Task<bool> Logout();
    }
}
