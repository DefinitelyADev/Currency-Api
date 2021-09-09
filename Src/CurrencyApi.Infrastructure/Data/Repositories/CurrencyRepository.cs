using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CurrencyRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public PagedResult<Currency> Find(Expression<Func<Currency, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Currency>> FindAsync(Expression<Func<Currency, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Currency GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Currency Add(Currency item)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> AddAsync(Currency item)
        {
            throw new NotImplementedException();
        }

        public Currency Update(Currency item)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> UpdateAsync(Currency item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
