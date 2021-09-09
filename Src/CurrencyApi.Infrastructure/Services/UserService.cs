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

        public PagedResult<User> Get(GetUserRequest request) => _unitOfWork.Users.Find(GetExpressionFromRequest(request));

        public async Task<PagedResult<User>> GetAsync(GetUserRequest request) => await _unitOfWork.Users.FindAsync(GetExpressionFromRequest(request));

        public User? GetById(string id) => _unitOfWork.Users.GetById(id);

        public async Task<User?> GetByIdAsync(string id) => await _unitOfWork.Users.GetByIdAsync(id);

        public CreateUserResult Create(CreateUserRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateUserResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            User newUser = new(request.Username);

            User createdUser = _unitOfWork.Users.Add(newUser);

            _unitOfWork.Commit();

            return new CreateUserResult {Data = createdUser};
        }

        public async Task<CreateUserResult> CreateAsync(CreateUserRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateUserResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            User newUser = new(request.Username);

            User createdUser = await _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.CommitAsync();

            return new CreateUserResult {Data = createdUser};
        }

        public DeleteUserResult Delete(string id)
        {
            ValidationResult validationResult = ValidateDeleteRequest(id);

            if (validationResult.HasErrors)
                return new DeleteUserResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            bool result = _unitOfWork.Users.Remove(id);

            _unitOfWork.Commit();

            return new DeleteUserResult {Succeeded = result};
        }

        public async Task<DeleteUserResult> DeleteAsync(string id)
        {
            ValidationResult validationResult = ValidateDeleteRequest(id);

            if (validationResult.HasErrors)
                return new DeleteUserResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            bool result = await _unitOfWork.Users.RemoveAsync(id);

            await _unitOfWork.CommitAsync();

            return new DeleteUserResult {Succeeded = result};
        }

        public UpdatePasswordResult UpdatePassword(string username, ChangePasswordRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new UpdatePasswordResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            User? user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
                return new UpdatePasswordResult {Errors = new List<string> {"User does not exist."}, Succeeded = false};

            IdentityResult result = _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword).Result;

            if (!result.Succeeded)
                throw new IdentityResultException(result.Errors);

            return new UpdatePasswordResult {Succeeded = result.Succeeded, Errors = result.Errors.Select(error => error.Description).ToList()};
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
            Expression<Func<User, bool>> expression = user => ObjectHelper.IsNull(request.Username) || user.UserName.Contains(request.Username);
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

        private ValidationResult ValidateDeleteRequest(string id)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(id))
                result.AddError("Id cannot be empty.");

            return result;
        }
    }
}
