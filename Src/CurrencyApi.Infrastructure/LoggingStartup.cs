using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CurrencyApi.Infrastructure
{
    public class LoggingStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseSerilogRequestLogging();
        }

        public int Order => 300;
    }
}
