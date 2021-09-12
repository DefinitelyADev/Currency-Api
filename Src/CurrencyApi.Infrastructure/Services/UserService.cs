using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.User;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.UserResults;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CurrencyApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<PagedResult<User>> GetAsync(GetUserRequest request) => await _unitOfWork.Users.FindAsync(request, GetExpressionFromRequest(request));

        public async Task<User> GetByIdAsync(string id) => await _unitOfWork.Users.GetByIdAsync(id);

        public async Task<CreateUserResult> CreateAsync(CreateUserRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateUserResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            User user = new(request.Username);

            IdentityResult creationResult = await _userManager.CreateAsync(user, request.Password);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "user");

            if (creationResult.Succeeded && roleResult.Succeeded)
                return new CreateUserResult {Data = user, Succeeded = true};

            List<string> errors = creationResult.Errors
                .Select(error => error.Description)
                .Concat(roleResult.Errors.Select(error => error.Description))
                .ToList();

            return new CreateUserResult
            {
                Errors = errors,
                Succeeded = false
            };

        }

        public async Task<DeleteUserResult> DeleteAsync(string id)
        {
            bool result = await _unitOfWork.Users.RemoveAsync(id);

            await _unitOfWork.CommitAsync();

            return new DeleteUserResult {Succeeded = result};
        }

        public async Task<UpdatePasswordResult> UpdatePasswordAsync(string username, ChangePasswordRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new UpdatePasswordResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            User? user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return new UpdatePasswordResult {Errors = new List<string> {"User does not exist."}, Succeeded = false};

            IdentityResult result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

            if (!result.Succeeded)
                throw new IdentityResultException(result.Errors);

            return new UpdatePasswordResult {Succeeded = result.Succeeded, Errors = result.Errors.Select(error => error.Description).ToList()};
        }

        private Expression<Func<User, bool>> GetExpressionFromRequest(GetUserRequest request)
        {
            Expression<Func<User, bool>> expression = user => ObjectHelper.IsNull(request.Username) || user.NormalizedUserName.Contains(request.Username.ToUpper());
            return expression;
        }

        private ValidationResult ValidateRequest(CreateUserRequest request)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(request.Username))
                result.AddError("Username must be between 8 and 30 characters, start with the following: upper case (A-Z), lower case (a-z) and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and an underscore character (e.g. _).");

            if (CommonHelper.IsValidPassword(request.Password))
                result.AddError("Password must be at least 8 characters and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*-).");

            if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
                result.AddError("Password confirmation cannot be empty.");

            if (request.ConfirmPassword.Equals(request.Password))
                result.AddError("Password and password confirmation must match.");

            return result;
        }

        private ValidationResult ValidateRequest(ChangePasswordRequest request)
        {
            ValidationResult result = new();

            if (CommonHelper.IsValidPassword(request.NewPassword))
                result.AddError("Password must be at least 8 characters and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*-).");

            if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
                result.AddError("Password confirmation cannot be empty.");

            if (request.ConfirmPassword.Equals(request.NewPassword))
                result.AddError("Password and password confirmation must match.");

            return result;
        }
    }
}
