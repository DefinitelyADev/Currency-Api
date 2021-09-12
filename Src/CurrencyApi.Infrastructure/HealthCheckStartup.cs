using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class HealthCheckStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks("/health");
        }

        public int Order => 300;
    }
}
