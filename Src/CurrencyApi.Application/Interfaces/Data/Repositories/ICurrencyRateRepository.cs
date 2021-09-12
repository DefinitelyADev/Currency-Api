using System.Threading.Tasks;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface ICurrencyRateRepository : IRepository<int, CurrencyRate>
    {
        Task<CurrencyRate> GetByIdsAsync(int originCurrencyId, int targetCurrencyId);
    }
}
