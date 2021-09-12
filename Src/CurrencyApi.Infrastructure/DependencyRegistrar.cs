using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Infrastructure.Data;
using CurrencyApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, ITypeFinder typeFinder)
        {
            //repositories
            // services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            // services.AddScoped<ITokenRepository, TokenRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();

            //UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICurrencyRateService, CurrencyRateService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IUserService, UserService>();

        }

        public int Order => 1;
    }
}
