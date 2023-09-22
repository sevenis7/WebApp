using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.ViewModels;
using System.Security.Claims;
using WebAppApi.Authenticators;
using WebAppApi.Requests;
using WebAppApi.Responses;
using WebAppApi.TokenGenerators;
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
        public IActionResult Register([FromBody] UserRegisterViewModel model)
        {
            var user = _accountService.Register(model);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult GetToken([FromBody] UserLoginViewModel model)
       {
            var user = _accountService.Login(model);

            if (user == null)
            {
                return BadRequest();
            }

            var response = _authenticator.Authenticate(user);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            var isValidRefreshToken = _refreshTokenValidator.Validate(request.RefreshToken);

            if (!isValidRefreshToken)
            {
                return BadRequest();
            }

            var user = _accountService.GetUserByToken(request.RefreshToken, out RefreshToken refreshToken);

            if (user == null)
            {
                return BadRequest();
            }

            _accountService.DeleteTokenById(refreshToken.Id);

            var response = _authenticator.Authenticate(user);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            string rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            _accountService.Logout(userId);

            return Ok();
        }
    }
}
