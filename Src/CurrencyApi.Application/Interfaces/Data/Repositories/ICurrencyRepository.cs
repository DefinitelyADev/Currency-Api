using CurrencyApi.Application.Interfaces.Data.Core;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface ICurrencyRepository : IRepository<int, Currency>
    {

    }
}
