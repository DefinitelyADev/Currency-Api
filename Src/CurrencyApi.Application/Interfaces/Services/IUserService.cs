using System.Threading.Tasks;
using CurrencyApi.Application.Requests.User;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.UserResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        PagedResult<User> Get(GetUserRequest request);
        Task<PagedResult<User>> GetAsync(GetUserRequest request);
        User? GetById(string id);
        Task<User?> GetByIdAsync(string id);
        CreateUserResult Create(CreateUserRequest request);
        Task<CreateUserResult> CreateAsync(CreateUserRequest request);
        DeleteUserResult Delete(string id);
        Task<DeleteUserResult> DeleteAsync(string id);
        UpdatePasswordResult UpdatePassword(string username, ChangePasswordRequest request);
        Task<UpdatePasswordResult> UpdatePasswordAsync(string username, ChangePasswordRequest request);
    }
}
