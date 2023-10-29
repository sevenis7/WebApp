using Blazored.LocalStorage;
using ServiceLayer.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using ServiceLayer.Responses;
using DataLayer.Entities;
using WebClient.Services.Interfaces;

namespace WebClient.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILocalStorageService _localStorageService;
        private const string JWT = nameof(JWT);

        private string? _jwt = "";
        private Role? role;

        public event Action<string?>? LoginChange;

        public AccountService(IHttpClientFactory factory, ILocalStorageService localStorageService)
        {
            _factory = factory;
            _localStorageService = localStorageService;
        }

        public async Task<DateTime> Login(UserLoginViewModel model)
        {
            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("account/login", model);

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Login failed");

            var content = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();

            if (content == null)
                throw new InvalidDataException();

            _jwt = content.AccessToken;

            await _localStorageService.SetItemAsStringAsync(JWT, content.AccessToken);

            LoginChange?.Invoke(GetUserName(content.AccessToken));

            return new JwtSecurityToken(_jwt).ValidTo;
        }

        public async Task<User> Register(UserRegisterViewModel model)
        {
            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("account/register", model);

            if (!response.IsSuccessStatusCode)
                throw new InvalidDataException();

            var content = await response.Content.ReadFromJsonAsync<User>();

            if (content == null)
                throw new InvalidDataException();

            return content;
        }

        private static string GetUserName(string token)
        {
            return new JwtSecurityToken(token).Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }

        public async Task<string?> GetJwt()
        {
            if (string.IsNullOrEmpty(_jwt))
                _jwt = await _localStorageService.GetItemAsStringAsync(JWT);

            return _jwt;
        }

        public async Task<Role?>? GetRole()
        {
            if (role == null)
            {
                var jwt = await GetJwt();

                var token = new JwtSecurityToken(jwt);

                var roleRaw = token.Claims.First(c => c.Type == ClaimTypes.Role).Value;

                role = Enum.Parse<Role>(roleRaw);
            }

            return role;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(JWT);

            _jwt = null;

            LoginChange?.Invoke(null);
        }

    }
}
