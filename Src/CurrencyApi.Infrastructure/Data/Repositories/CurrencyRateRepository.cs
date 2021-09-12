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
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CurrencyRateRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedResult<CurrencyRate>> FindAsync(PagedRequest paginationParams, Expression<Func<CurrencyRate, bool>> predicate)
        {
            int totalCount = await _dbContext.CurrencyRates.CountAsync(predicate);
            List<CurrencyRate>? data = await _dbContext.CurrencyRates.Where(predicate).ApplyPaginationParameters(paginationParams).ToListAsync();

            if (data == null || !data.Any())
                throw new RecordNotFoundException();

            return new PagedResult<CurrencyRate>(data, totalCount);
        }

        public async Task<CurrencyRate> GetByIdAsync(int id)
        {
            CurrencyRate? data = await _dbContext.CurrencyRates.FirstOrDefaultAsync(currencyRate => currencyRate.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }

        public async Task<CurrencyRate> AddAsync(CurrencyRate item)
        {
            await _dbContext.CurrencyRates.AddAsync(item);
            return item;
        }

        public async Task<CurrencyRate> UpdateAsync(CurrencyRate item)
        {
            CurrencyRate? data = await _dbContext.CurrencyRates.FirstOrDefaultAsync(rate => rate.Id == item.Id);

            if (data == null)
                throw new RecordNotFoundException();

            data.Rate = item.Rate;
            data.OriginCurrencyId = item.OriginCurrencyId;
            data.TargetCurrencyId = item.TargetCurrencyId;

            return data;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CurrencyRate? data = await _dbContext.CurrencyRates.FirstOrDefaultAsync(currency => currency.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            _dbContext.CurrencyRates.Remove(data);

            return true;
        }

        public async Task<CurrencyRate> GetByIdsAsync(int originCurrencyId, int targetCurrencyId)
        {
            CurrencyRate? data = await _dbContext.CurrencyRates
                .Include(rate => rate.OriginCurrency)
                .Include(rate => rate.TargetCurrency)
                .FirstOrDefaultAsync(currencyRate => currencyRate.OriginCurrencyId == originCurrencyId && currencyRate.TargetCurrencyId == targetCurrencyId);

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }
    }
}
