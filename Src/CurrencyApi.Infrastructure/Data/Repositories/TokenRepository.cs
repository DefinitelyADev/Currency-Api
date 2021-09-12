using System.Threading.Tasks;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TokenRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<RefreshToken> GetAsync(string id)
        {
            RefreshToken? data = await _dbContext.RefreshTokens.FirstOrDefaultAsync(token => token.Token == id);

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken item)
        {
            await _dbContext.RefreshTokens.AddAsync(item);

            return item;
        }

        public async Task<RefreshToken> UpdateAsync(RefreshToken item)
        {
            RefreshToken? data = await _dbContext.RefreshTokens.FirstOrDefaultAsync(token => token.Token == item.Token);

            if (data == null)
                throw new RecordNotFoundException();

            data.Invalidated = item.Invalidated;
            data.Used = item.Used;

            return data;
        }
    }
}
