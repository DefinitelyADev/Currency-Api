using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Data.Repositories;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Data.Contexts;

namespace CurrencyApi.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public Task<PagedResult<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
