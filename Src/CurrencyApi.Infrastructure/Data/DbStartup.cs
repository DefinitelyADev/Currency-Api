using System;
using CurrencyApi.Application.Helpers;
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
            DataSettings? settings = GetSettingsFromMigrationArgs(configuration);

            bool isMigrationBeingAdded = settings != null;

            // Override settings from config if migration add is in progress
            switch (isMigrationBeingAdded)
            {
                case false when !DataSettingsManager.IsDatabaseInstalled():
                    return;
                case false:
                    settings = DataSettingsManager.LoadSettings();
                    break;
            }

            if (settings == null)
                return;

            services.AddEntityFrameworkCore(settings);
            services.AddEntityFrameworkCoreIdentity();
        }

        public void Configure(IApplicationBuilder application) { }

        public int Order => 10;

        private static DataSettings? GetSettingsFromMigrationArgs(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue("ConnectionString", "");

            string providerString = configuration.GetValue("Provider", "");

            if (ObjectHelper.IsNull(connectionString) || ObjectHelper.IsNull(providerString))
                return null;

            DataProviders provider = ParseProviderString(providerString);

            // Enable verbose logging by default when Ef core is performing migrations
            bool enableSensitiveDataLogging = configuration.GetValue("EnableSensitiveDataLogging", true);

            return new DataSettings
            {
                ConnectionString = connectionString,
                DataProvider = provider,
                EnableSensitiveDataLogging = enableSensitiveDataLogging
            };
        }

        private static DataProviders ParseProviderString(string providerString)
        {
            try
            {
                DataProviders provider = (DataProviders)Enum.Parse(typeof(DataProviders), providerString, true);
                return provider;
            }
            catch (ArgumentException)
            {
                // ignored
            }

            return DataProviders.Unknown;
        }
    }
}
