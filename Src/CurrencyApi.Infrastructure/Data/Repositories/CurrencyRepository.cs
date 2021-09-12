using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Extensions;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Application.Requests;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CurrencyRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedResult<Currency>> FindAsync(PagedRequest paginationParams, Expression<Func<Currency, bool>> predicate)
        {
            int totalCount = await _dbContext.Currencies.CountAsync(predicate);
            List<Currency>? data = await _dbContext.Currencies.Where(predicate).ApplyPaginationParameters(paginationParams).ToListAsync();

            if (data == null || !data.Any())
                throw new RecordNotFoundException();

            return new PagedResult<Currency>(data, totalCount) { Succeeded = true };
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            Currency? data = await _dbContext.Currencies.FirstOrDefaultAsync(currency => currency.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }

        public async Task<Currency> AddAsync(Currency item)
        {
            await _dbContext.Currencies.AddAsync(item);
            return item;
        }

        public async Task<Currency> UpdateAsync(Currency item)
        {
            Currency? data = await _dbContext.Currencies.FirstOrDefaultAsync(currency => currency.Id == item.Id);

            if (data == null)
                throw new RecordNotFoundException();

            data.Name = item.Name;
            data.AlphabeticCode = item.AlphabeticCode;
            data.NumericCode = item.NumericCode;
            data.DecimalDigits = item.DecimalDigits;

            return data;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Currency? data = await _dbContext.Currencies.FirstOrDefaultAsync(currency => currency.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            _dbContext.Currencies.Remove(data);

            return true;
        }
    }
}
