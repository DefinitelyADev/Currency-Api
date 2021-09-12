using System.Linq;
using CurrencyApi.Application.Requests;

namespace CurrencyApi.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaginationParameters<T>(this IQueryable<T> source, PagedRequest paginationParams)
        {
            if (paginationParams.Skip != null)
                source = source.Skip(paginationParams.Skip.Value);

            if (paginationParams.Take != null)
                source = source.Take(paginationParams.Take.Value);

            return source;
        }
    }
}
