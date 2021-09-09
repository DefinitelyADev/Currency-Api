using System;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Core.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyApi.Infrastructure
{
    public class ErrorHandlerStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

        public void Configure(IApplicationBuilder application)
        {
            IWebHostEnvironment? webHostEnvironment = EngineContext.Current.Resolve<IWebHostEnvironment>();
            if (webHostEnvironment == null)
            {
                throw new InvalidOperationException("WebHostEnvironment cannot be null");
            }
            application.UseExceptionHandler(webHostEnvironment.IsDevelopment() ? "/error-local-development" : "/error");
        }

        public int Order => 0; //error handlers should be loaded first
    }
}
