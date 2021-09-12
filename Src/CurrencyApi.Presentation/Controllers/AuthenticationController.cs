using System.Threading.Tasks;
using CurrencyApi.Application.Requests.Authentication;
using CurrencyApi.Application.Results.AuthenticationResults;
using CurrencyApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : BaseApiController
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            AuthenticationResult result = await _authenticationService.LoginAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            AuthenticationResult result = await _authenticationService.RefreshTokenAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
