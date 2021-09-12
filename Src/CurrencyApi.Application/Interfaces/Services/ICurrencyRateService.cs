using System.Threading.Tasks;
using CurrencyApi.Application.Requests.CurrencyRate;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyRateResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface ICurrencyRateService
    {
        Task<PagedResult<CurrencyRate>> GetAsync(GetCurrencyRateRequest request);
        Task<CurrencyRate> GetByIdAsync(int id);
        Task<CurrencyRate> GetByIdsAsync(int originCurrencyId, int targetCurrencyId);
        Task<CalculationResult> CalculateByIdsAsync(int originCurrencyId, int targetCurrencyId, decimal amount);
        Task<CreateCurrencyRateResult> CreateAsync(CreateCurrencyRateRequest request);
        Task<UpdateCurrencyRateResult> UpdateAsync(int id, UpdateCurrencyRateRequest request);
        Task<DeleteCurrencyRateResult> DeleteAsync(int id);
    }
}
