using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.CurrencyRate;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyRateResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Infrastructure.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyRateService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<PagedResult<CurrencyRate>> GetAsync(GetCurrencyRateRequest request) => await _unitOfWork.CurrencyRates.FindAsync(request, GetExpressionFromRequest(request));

        public async Task<CurrencyRate> GetByIdAsync(int id) => await _unitOfWork.CurrencyRates.GetByIdAsync(id);

        public async Task<CurrencyRate> GetByIdsAsync(int originCurrencyId, int targetCurrencyId) => await _unitOfWork.CurrencyRates.GetByIdsAsync(originCurrencyId, targetCurrencyId);

        public async Task<CalculationResult> CalculateByIdsAsync(int originCurrencyId, int targetCurrencyId, decimal amount)
        {
            CurrencyRate currencyRate = await GetByIdsAsync(originCurrencyId, targetCurrencyId);

            decimal result = amount * currencyRate.Rate;

            return new CalculationResult {Result = result};
        }

        public async Task<CreateCurrencyRateResult> CreateAsync(CreateCurrencyRateRequest request)
        {
            CurrencyRate newCurrencyRate = new() {Rate = request.Rate, OriginCurrencyId = request.CurrencyId, TargetCurrencyId = request.TargetCurrencyId};

            CurrencyRate createdCurrencyRate = await _unitOfWork.CurrencyRates.AddAsync(newCurrencyRate);

            await _unitOfWork.CommitAsync();

            return new CreateCurrencyRateResult {Data = createdCurrencyRate};
        }

        public async Task<UpdateCurrencyRateResult> UpdateAsync(UpdateCurrencyRateRequest request)
        {
            CurrencyRate currencyRateToUpdate = new() {Rate = request.Rate, OriginCurrencyId = request.CurrencyId, TargetCurrencyId = request.TargetCurrencyId};

            CurrencyRate createdCurrencyRate = await _unitOfWork.CurrencyRates.UpdateAsync(currencyRateToUpdate);

            await _unitOfWork.CommitAsync();

            return new UpdateCurrencyRateResult {Data = createdCurrencyRate};
        }

        public async Task<DeleteCurrencyRateResult> DeleteAsync(int id)
        {
            bool result = await _unitOfWork.Currencies.RemoveAsync(id);

            await _unitOfWork.CommitAsync();

            return new DeleteCurrencyRateResult {Succeeded = result};
        }

        private Expression<Func<CurrencyRate, bool>> GetExpressionFromRequest(GetCurrencyRateRequest request)
        {
            Expression<Func<CurrencyRate, bool>> expression = currencyRate =>
                (ObjectHelper.IsNull(request.OriginCurrencyName) || currencyRate.OriginCurrency != null && currencyRate.OriginCurrency.Name.ToUpper().Contains(request.OriginCurrencyName.ToUpper()))
                && (ObjectHelper.IsNull(request.OriginCurrencyAlphabeticCode) || currencyRate.OriginCurrency != null && currencyRate.OriginCurrency.AlphabeticCode.ToUpper().Contains(request.OriginCurrencyAlphabeticCode.ToUpper()))
                && (ObjectHelper.IsNull(request.OriginCurrencyNumericCode) || currencyRate.OriginCurrency != null && currencyRate.OriginCurrency.NumericCode == request.OriginCurrencyNumericCode)
                && (ObjectHelper.IsNull(request.TargetCurrencyName) || currencyRate.TargetCurrency != null && currencyRate.TargetCurrency.Name.ToUpper().Contains(request.TargetCurrencyName.ToUpper()))
                && (ObjectHelper.IsNull(request.TargetCurrencyAlphabeticCode) || currencyRate.TargetCurrency != null && currencyRate.TargetCurrency.AlphabeticCode.ToUpper().Contains(request.TargetCurrencyAlphabeticCode.ToUpper()))
                && (ObjectHelper.IsNull(request.TargetCurrencyNumericCode) || currencyRate.TargetCurrency != null && currencyRate.TargetCurrency.NumericCode == request.TargetCurrencyNumericCode);

            return expression;
        }
    }
}
