using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TokenRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public RefreshToken Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<RefreshToken> GetAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public RefreshToken Add(RefreshToken item)
        {
            throw new System.NotImplementedException();
        }

        public Task<RefreshToken> AddAsync(RefreshToken item)
        {
            throw new System.NotImplementedException();
        }

        public RefreshToken Update(RefreshToken item)
        {
            throw new System.NotImplementedException();
        }

        public Task<RefreshToken> UpdateAsync(RefreshToken item)
        {
            throw new System.NotImplementedException();
        }
    }
}
