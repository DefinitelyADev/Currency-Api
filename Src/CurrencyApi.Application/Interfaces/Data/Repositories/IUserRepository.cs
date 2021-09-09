using System.Threading.Tasks;
using CurrencyApi.Application.Results.UserResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface IUserRepository : IRepository<string, User>
    {
        User GetByUsername(string username);
        Task<User> GetByUsernameAsync(string username);
    }
}
