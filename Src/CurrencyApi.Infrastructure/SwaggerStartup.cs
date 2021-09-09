using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class SwaggerStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

        public void Configure(IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyApi.Presentation v1"));
        }

        public int Order => 200;
    }
}
