using DataLayer.Entities;
using DataLayer.Repositories;
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

        public User Register(UserRegisterViewModel model)
        {
            var existingUserByEmail = _userRepository.GetByEmail(model.Email);
            var exisringUserByLogin = _userRepository.GetByLogin(model.Login);

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

            _userRepository.Add(user);

            return user;
        }

        public User Login(UserLoginViewModel model)
        {
            var existingUserByLogin = _userRepository.GetByLogin(model.Login);

            if (existingUserByLogin == null)
            {
                return null;
            }

            if (!_passwordHasher.VerifyPassword(model.Password, existingUserByLogin.PasswordHash))
            {
                return null;
            }

            return existingUserByLogin;
        }

        public RefreshToken CreateRefreshToken(RefreshToken token)
        {
            token.Id = Guid.NewGuid();
            _refreshTokenRepository.Add(token);
            return token;
        }

        public User GetUserByToken(string rawToken, out RefreshToken refreshToken)
        {
            var token = _refreshTokenRepository.GetByToken(rawToken);

            refreshToken = token;

            if (token == null)
            {
                return null;
            }

            var existingUserById = _userRepository.GetById(token.User.UserId);

            if (existingUserById == null)
            {
                return null;
            }

            return existingUserById;
        }

        public void DeleteTokenById(Guid? id)
        {
            var existingTokenById = _refreshTokenRepository.GetById(id);

            if (existingTokenById == null)
            {
                return;
            }

            _refreshTokenRepository.Delete(existingTokenById);
        }

        public void DeleteTokenByToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool Logout(Guid id)
        {
            var existingUserById = _userRepository.GetById(id);

            if (existingUserById == null) return false;

            foreach (var e in existingUserById.RefreshTokens) _refreshTokenRepository.Delete(e);
            return true;
        }

    }
}
