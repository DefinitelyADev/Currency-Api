using System.Net;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Core.Engine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        /// <returns>Configured engine and app settings</returns>
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //let the operating system decide what TLS protocol version to use
            //see https://docs.microsoft.com/dotnet/framework/network-programming/tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //create default file provider
            CommonHelper.DefaultFileProvider = new WebAppFileProvider(webHostEnvironment);

            //add accessor to HttpContext
            // services.AddHttpContextAccessor();

            //add configuration parameters
            // var appSettings = new AppSettings();
            // configuration.Bind(appSettings);
            // services.AddSingleton(appSettings);
            // AppSettingsHelper.SaveAppSettings(appSettings);

            //initialize plugins
            // var mvcCoreBuilder = services.AddMvcCore();
            // mvcCoreBuilder.PartManager.InitializePlugins(appSettings);

            //create engine and configure service provider
            IEngine engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration);
            // engine.RegisterDependencies(services, appSettings);

            // return (engine, appSettings);
        }
    }
}
