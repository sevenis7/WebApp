using DataLayer.Entities;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Interfaces
{
    public interface IAccountService
    {
        Task<RefreshToken> CreateRefreshToken(RefreshToken token);
        Task DeleteTokenById(Guid id);
        Task DeleteToken(string token);
        Task<User?> GetUserByToken(string rawToken);
        Task<User?> Login(UserLoginViewModel model);
        Task<bool> Logout(Guid id);
        Task<User?> Register(UserRegisterViewModel model);
    }
}