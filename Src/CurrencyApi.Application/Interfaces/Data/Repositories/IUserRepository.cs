using System.Threading.Tasks;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface IUserRepository : IRepository<string, User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
