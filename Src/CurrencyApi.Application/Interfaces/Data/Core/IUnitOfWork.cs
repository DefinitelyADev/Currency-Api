using System.Threading.Tasks;

namespace CurrencyApi.Application.Interfaces.Data.Core
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
