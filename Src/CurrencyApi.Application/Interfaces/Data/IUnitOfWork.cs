using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data.Repositories;

namespace CurrencyApi.Application.Interfaces.Data
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        int Commit();
        Task<int> CommitAsync();
    }
}
