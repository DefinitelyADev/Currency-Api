using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Extensions;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Application.Requests;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<PagedResult<User>> FindAsync(PagedRequest paginationParams, Expression<Func<User, bool>> predicate)
        {
            int totalCount = await _dbContext.Users.CountAsync(predicate);
            List<User>? data = await _dbContext.Users.Where(predicate).ApplyPaginationParameters(paginationParams).ToListAsync();

            if (data == null || !data.Any())
                throw new RecordNotFoundException();

            return new PagedResult<User>(data, totalCount) { Succeeded = true };
        }

        public async Task<User> GetByIdAsync(string id)
        {
            User? data = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }

        public async Task<User> AddAsync(User item)
        {
            await _dbContext.Users.AddAsync(item);
            return item;
        }

        public Task<User> UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync(string id)
        {
            User? data = await _dbContext.Users.FirstOrDefaultAsync(currency => currency.Id == id);

            if (data == null)
                throw new RecordNotFoundException();

            _dbContext.Users.Remove(data);

            return true;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            User? data = await _dbContext.Users.FirstOrDefaultAsync(user => user.NormalizedUserName == username.ToUpper());

            if (data == null)
                throw new RecordNotFoundException();

            return data;
        }
    }
}
