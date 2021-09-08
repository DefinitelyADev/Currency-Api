using System.Threading.Tasks;
using CurrencyApi.Application.Requests.User;
using CurrencyApi.Application.Results;

namespace CurrencyApi.Application.Interfaces.Services
{
    public interface IUserService
    {
        UpdatePasswordResult UpdatePassword(string username, ChangePasswordRequest request);
        Task<UpdatePasswordResult> UpdatePasswordAsync(string username, ChangePasswordRequest request);
    }
}
