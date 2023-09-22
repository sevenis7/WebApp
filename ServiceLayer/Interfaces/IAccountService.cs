using DataLayer.Entities;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Interfaces
{
    public interface IAccountService
    {
        User Register(UserRegisterViewModel model);

        User Login(UserLoginViewModel model);

        bool Logout(Guid id);

        User GetUserByToken(string token, out RefreshToken refreshToken);

        RefreshToken CreateRefreshToken(RefreshToken token);

        void DeleteTokenById(Guid? id);

    }
}
