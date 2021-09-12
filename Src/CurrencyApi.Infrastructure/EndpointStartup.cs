using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.ActionFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class EndpointStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new RecordNotFoundExceptionFilter());
                options.Filters.Add(new IdentityResultExceptionFilter());
            });
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public int Order => 1000; // Endpoints should be loaded last
    }
}
