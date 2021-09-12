using System.Threading.Tasks;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface ITokenRepository
    {
        Task<RefreshToken> GetAsync(string id);
        Task<RefreshToken> AddAsync(RefreshToken item);
        Task<RefreshToken> UpdateAsync(RefreshToken item);
    }
}
