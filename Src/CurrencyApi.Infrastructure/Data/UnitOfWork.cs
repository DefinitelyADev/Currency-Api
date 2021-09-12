using System;
using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Infrastructure.Data.Contexts;
using CurrencyApi.Infrastructure.Data.Repositories;

namespace CurrencyApi.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Tokens = new TokenRepository(_dbContext);
            Users = new UserRepository(_dbContext);
            Currencies = new CurrencyRepository(_dbContext);
        }

        public ITokenRepository Tokens { get; }
        public IUserRepository Users { get; }
        public ICurrencyRepository Currencies { get; }
        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            // _dbContext.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
