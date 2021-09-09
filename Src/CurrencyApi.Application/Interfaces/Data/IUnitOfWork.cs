using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data.Repositories;

namespace CurrencyApi.Application.Interfaces.Data
{
    public interface IUnitOfWork
    {
        ITokenRepository Tokens { get; }
        IUserRepository Users { get; }
        ICurrencyRepository Currencies { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
