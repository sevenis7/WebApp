using DataLayer.Entities;
using ServiceLayer.ViewModels;

namespace WebClient.Services.Interfaces
{
    public interface IAccountService
    {
        event Action<string?>? LoginChange;
        Task<string?> GetJwt();
        Task<DateTime> Login(UserLoginViewModel model);
        Task<User> Register(UserRegisterViewModel model);
        Task Logout();

    }
}