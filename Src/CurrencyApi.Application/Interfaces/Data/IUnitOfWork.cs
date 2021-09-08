using CurrencyApi.Application.Interfaces.Data.Repositories;

namespace CurrencyApi.Application.Interfaces.Data
{
    public interface IUnitOfWork : Core.IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
    }
}
