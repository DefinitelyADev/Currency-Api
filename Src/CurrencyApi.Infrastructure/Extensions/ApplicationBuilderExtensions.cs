using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Core.Engine;
using CurrencyApi.Infrastructure.Data.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace CurrencyApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        public static void StartEngine(this IApplicationBuilder application)
        {
            IEngine engine = EngineContext.Current;

            //further actions are performed only when the database is installed
            if (DataSettingsManager.IsDatabaseInstalled())
            {
#if !DEBUG      //prevent save the update migrations into the DB during the developing process
                ApplicationDbContext dbContext = engine.ResolveRequired<ApplicationDbContext>();
                dbContext.Database.MigrateAsync().Wait();
#endif

                //log application start
                engine.Resolve<ILogger<EngineContext>>().LogInformation("Application started...");
            }
        }
    }
}
