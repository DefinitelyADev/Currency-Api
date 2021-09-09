using System.Threading.Tasks;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface ITokenRepository
    {
        RefreshToken Get(string id);
        Task<RefreshToken> GetAsync(string id);

        RefreshToken Add(RefreshToken item);
        Task<RefreshToken> AddAsync(RefreshToken item);

        RefreshToken Update(RefreshToken item);
        Task<RefreshToken> UpdateAsync(RefreshToken item);
    }
}
