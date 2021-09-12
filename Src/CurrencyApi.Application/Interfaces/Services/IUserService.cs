using System.Threading.Tasks;
using CurrencyApi.Application.Requests.User;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.UserResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<PagedResult<User>> GetAsync(GetUserRequest request);
        Task<User?> GetByIdAsync(string id);
        Task<CreateUserResult> CreateAsync(CreateUserRequest request);
        Task<DeleteUserResult> DeleteAsync(string id);
        Task<UpdatePasswordResult> UpdatePasswordAsync(string username, ChangePasswordRequest request);
    }
}
