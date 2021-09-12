using System;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Core.Engine;
using CurrencyApi.Infrastructure.Data.Contexts;
using CurrencyApi.Infrastructure.Data.Settings;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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

        public void Configure(IApplicationBuilder application)
        {

            //further actions are performed only when the database is installed
            if (DataSettingsManager.IsDatabaseInstalled())
            {
#if !DEBUG      //prevent save the update migrations into the DB during the developing process
                using IServiceScope? scope = EngineContext.Current.ServiceProvider?.CreateScope();

                if (scope == null)
                    throw new WebAppException("Service scope cannot be null during the migration process.");

                ApplicationDbContext? dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                if (dbContext == null)
                    throw new WebAppException("Application db context cannot be null during the migration process.");

                dbContext.Database.MigrateAsync().Wait();
#endif
            }
        }

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
