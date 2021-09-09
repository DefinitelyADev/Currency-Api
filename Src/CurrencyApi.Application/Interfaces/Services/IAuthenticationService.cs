using System.Threading.Tasks;
using CurrencyApi.Application.Requests.Authentication;
using CurrencyApi.Application.Results;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(LoginRequest request);
        Task<AuthenticationResult> LoginAsync(LoginRequest request);

        AuthenticationResult RefreshToken(RefreshTokenRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
