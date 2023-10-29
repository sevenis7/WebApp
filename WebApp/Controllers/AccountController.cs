using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.ViewModels;
using System.Security.Claims;
using WebAppApi.Authenticators;
using ServiceLayer.Responses;
using WebAppApi.Requests;
using WebAppApi.TokenValidators;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

        public AccountController(IAccountService accountService,
            RefreshTokenValidator refreshTokenValidator,
            Authenticator authenticator)
        {
            _accountService = accountService;
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterViewModel model)
        {
            var user = await _accountService.Register(model);

            if (user == null) return BadRequest();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedResponse>> GetToken([FromBody] UserLoginViewModel model)
       {
            var user = await _accountService.Login(model);

            if (user == null) return BadRequest();

            var response = await _authenticator.Authenticate(user);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticatedResponse>> Refresh([FromBody] RefreshRequest request)
        {
            var isValidRefreshToken = _refreshTokenValidator.Validate(request.RefreshToken);

            if (!isValidRefreshToken) return BadRequest();

            var user = await _accountService.GetUserByToken(request.RefreshToken);

            if (user == null) return BadRequest();

            await _accountService.DeleteToken(request.RefreshToken);

            var response = await _authenticator.Authenticate(user);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<ActionResult> Logout()
        {
            string? rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _accountService.Logout(userId);

            return Ok();
        }
    }
}
