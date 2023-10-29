using DataLayer.Entities;
using ServiceLayer.Interfaces;
using WebAppApi.TokenGenerators;
using ServiceLayer.Responses;

namespace WebAppApi.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IAccountService _accountService;

        public Authenticator(AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IAccountService accountService)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _accountService = accountService;
        }

        public async Task<AuthenticatedResponse> Authenticate(User user)
        {
            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDto = new RefreshToken
            {
                Token = refreshToken,
                User = user,
            };

            await _accountService.CreateRefreshToken(refreshTokenDto);

            return  new AuthenticatedResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

    }
}
