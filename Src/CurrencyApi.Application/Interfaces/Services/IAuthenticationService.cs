using System.Threading.Tasks;
using CurrencyApi.Application.Requests.Authentication;
using CurrencyApi.Application.Results.AuthenticationResults;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(LoginRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
