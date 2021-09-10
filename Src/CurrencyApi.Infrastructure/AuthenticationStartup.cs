using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class AuthenticationStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        public void Configure(IApplicationBuilder application)
        {

        }

        public int Order => 500; // Authentication should be loaded before endpoints
    }
}
