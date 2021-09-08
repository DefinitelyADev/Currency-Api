using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface ICurrencyService
    {
        PagedResult<Currency> Get(GetCurrencyRequest request);
        Task<PagedResult<Currency>> GetAsync(GetCurrencyRequest request);
        Currency? GetById(int id);
        Task<Currency?> GetByIdAsync(int id);
        CreateCurrencyResult Create(CreateCurrencyRequest request);
        Task<CreateCurrencyResult> CreateAsync(CreateCurrencyRequest request);
        UpdateCurrencyResult Update(UpdateCurrencyRequest request);
        Task<UpdateCurrencyResult> UpdateAsync(UpdateCurrencyRequest request);
        DeleteCurrencyResult Delete(int id);
        Task<DeleteCurrencyResult> DeleteAsync(int id);
    }
}
