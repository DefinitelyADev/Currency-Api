using System.Net;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Core.Engine;
using CurrencyApi.Infrastructure.Data;
using CurrencyApi.Infrastructure.Data.Contexts;
using CurrencyApi.Infrastructure.Data.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace CurrencyApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="webHostEnvironment">Hosting environment</param>
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //let the operating system decide what TLS protocol version to use
            //see https://docs.microsoft.com/dotnet/framework/network-programming/tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //create default file provider
            CommonHelper.DefaultFileProvider = new WebAppFileProvider(webHostEnvironment);

            //add configuration parameters
            // var appSettings = new AppSettings();
            // configuration.Bind(appSettings);
            // services.AddSingleton(appSettings);

            //create engine and configure service provider
            IEngine engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration);
        }

        /// <summary>
        /// Add entity framework core to the service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddEntityFrameworkCore(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(Configure);
        }

        public static void AddEntityFrameworkCoreIdentity(this IServiceCollection services) => services
            .AddIdentity<User, IdentityRole>(Configure)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        private static void Configure(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
        }

        private static void Configure(DbContextOptionsBuilder options)
        {
            DataSettings? settings = DataSettingsManager.LoadSettings();

            if (settings == null)
                return;

            if (settings.EnableSensitiveDataLogging)
                options.EnableSensitiveDataLogging();

            _ = settings.DataProvider switch
            {
                DataProviders.SqlServer => ConfigureSqlServer(options, settings.ConnectionString),
                DataProviders.Postgres => ConfigurePostgreSql(options, settings.ConnectionString),
                DataProviders.MySql => ConfigureMySql(options, settings.ConnectionString),
                _ => throw new WebAppException($"Not supported data provider name: '{settings.DataProvider}'")
            };
        }

        private static DbContextOptionsBuilder ConfigureSqlServer(DbContextOptionsBuilder options, string connectionString)
        {
            // options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ArtemisPlatform.Presentation.Server"));
            options.UseSqlServer(connectionString);
            return options;
        }

        private static DbContextOptionsBuilder ConfigurePostgreSql(DbContextOptionsBuilder options, string connectionString)
        {
            // options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ArtemisPlatform.Presentation.Server"));
            options.UseNpgsql(connectionString);
            return options;
        }

        private static DbContextOptionsBuilder ConfigureMySql(DbContextOptionsBuilder options, string connectionString)
        {
            options.ReplaceService<ISqlGenerationHelper, ApplicationMySqlSqlGenerationHelper>();
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b =>
            {
                b.SchemaBehavior(MySqlSchemaBehavior.Translate, (_, objectName) => objectName);
                // b.MigrationsAssembly("ArtemisPlatform.Presentation.Server");
            });
            return options;
        }
    }
}
