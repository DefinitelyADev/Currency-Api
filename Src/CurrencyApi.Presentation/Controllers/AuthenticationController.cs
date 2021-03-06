using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Authentication;
using CurrencyApi.Application.Results.AuthenticationResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    [Route("[controller]"), AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            AuthenticationResult result = await _authenticationService.LoginAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            AuthenticationResult result = await _authenticationService.RefreshTokenAsync(request);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
