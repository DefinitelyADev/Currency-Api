using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class EndpointStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public int Order => 1000; // Endpoints should be loaded last
    }
}
