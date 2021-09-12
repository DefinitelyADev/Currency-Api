using System.Threading.Tasks;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface ICurrencyService
    {
        Task<PagedResult<Currency>> GetAsync(GetCurrencyRequest request);
        Task<Currency> GetByIdAsync(int id);
        Task<CreateCurrencyResult> CreateAsync(CreateCurrencyRequest request);
        Task<UpdateCurrencyResult> UpdateAsync(int id, UpdateCurrencyRequest request);
        Task<DeleteCurrencyResult> DeleteAsync(int id);
    }
}
