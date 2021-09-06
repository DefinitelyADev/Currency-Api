using Microsoft.AspNetCore.Builder;

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
            // EngineContext.Current.ConfigureRequestPipeline(application);
        }
        
        public static void StartEngine(this IApplicationBuilder application)
        {
            }
    }
}