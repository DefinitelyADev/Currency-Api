using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Results;

namespace CurrencyApi.Application.Interfaces.Data.Repositories
{
    public interface IRepository<in TKey, TEntity> where TEntity : class
    {
        PagedResult<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);

        TEntity Add(TEntity item);
        Task<TEntity> AddAsync(TEntity item);

        TEntity Update(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);

        bool Remove(TKey id);
        Task<bool> RemoveAsync(TKey id);
    }
}
