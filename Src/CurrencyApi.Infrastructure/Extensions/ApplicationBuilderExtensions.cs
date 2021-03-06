using CurrencyApi.Infrastructure.Core.Engine;
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
        public static void ConfigureRequestPipeline(this IApplicationBuilder application) => EngineContext.Current.ConfigureRequestPipeline(application);

        public static void StartEngine(this IApplicationBuilder application)
        {
            EngineContext.Current.Resolve<ILogger<EngineContext>>().LogInformation("Application started...");
        }
    }
}
