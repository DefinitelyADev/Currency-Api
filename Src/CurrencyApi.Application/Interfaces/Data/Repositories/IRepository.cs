using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Results;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface IRepository<in TKey, TEntity> where TEntity : class
    {
        Task<PagedResult<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> AddAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<bool> RemoveAsync(TKey id);
    }
}
