using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Data.Settings;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure.Data
{
    public class DbStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (!DataSettingsManager.IsDatabaseInstalled())
            {
                return;
            }

            services.AddEntityFrameworkCore();
            services.AddEntityFrameworkCoreIdentity();
        }

        public void Configure(IApplicationBuilder application) { }

        public int Order => 10;
    }
}
