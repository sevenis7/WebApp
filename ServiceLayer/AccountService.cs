using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using ServiceLayer.PasswordHashers;
using ServiceLayer.ViewModels;

namespace ServiceLayer
{

    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AccountService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<User?> Register(UserRegisterViewModel model)
        {
            var existingUserByEmail = await _userRepository.GetByEmail(model.Email!);
            var exisringUserByLogin = await _userRepository.GetByLogin(model.Login!);

            if (existingUserByEmail != null || exisringUserByLogin != null)
            {
                return null;
            }

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Login = model.Login,
                Email = model.Email,
                PasswordHash = _passwordHasher.HashPassword(model.Password),
                Role = Role.User,
            };

            await _userRepository.Add(user);

            return user;
        }

        public async Task<User?> Login(UserLoginViewModel model)
        {
            var existingUserByLogin = await _userRepository.GetByLogin(model.Login!);

            if (existingUserByLogin == null)
            {
                return null;
            }

            if (!_passwordHasher.VerifyPassword(model.Password!, existingUserByLogin.PasswordHash))
            {
                return null;
            }

            return existingUserByLogin;
        }

        public async Task<RefreshToken> CreateRefreshToken(RefreshToken token)
        {
            token.Id = Guid.NewGuid();

            await _refreshTokenRepository.Add(token);

            return token;
        }

        public async Task<User?> GetUserByToken(string rawToken)
        {
            var token = await _refreshTokenRepository.GetByToken(rawToken);

            if (token == null)
            {
                return null;
            }

            var existingUserById = await _userRepository.GetById(token.User.UserId);

            if (existingUserById == null)
            {
                return null;
            }

            return existingUserById;
        }

        public async Task DeleteTokenById(Guid id)
        {
            var existingTokenById = await _refreshTokenRepository.GetById(id);

            if (existingTokenById == null)
            {
                return;
            }

            await _refreshTokenRepository.Delete(existingTokenById);
        }

        public async Task DeleteToken(string token)
        {
            var existingToken = await _refreshTokenRepository.GetByToken(token);

            if (existingToken == null) return;

            await _refreshTokenRepository.Delete(existingToken);
        }

        public async Task<bool> Logout(Guid id)
        {
            var existingUserById = await _userRepository.GetById(id);

            if (existingUserById == null) return false;

            foreach (var e in existingUserById.RefreshTokens!) await _refreshTokenRepository.Delete(e);

            return true;
        }

    }
}
