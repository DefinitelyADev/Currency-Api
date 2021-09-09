using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class RoutingStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

        public void Configure(IApplicationBuilder application) => application.UseRouting();

        public int Order => 400; // Routing should be loaded before authentication
    }
}
