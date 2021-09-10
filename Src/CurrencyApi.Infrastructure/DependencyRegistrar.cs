using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Infrastructure.Data;
using CurrencyApi.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services, ITypeFinder typeFinder)
        {
            //repositories
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public int Order => 1;
    }
}
