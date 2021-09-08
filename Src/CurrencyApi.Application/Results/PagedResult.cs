using System.Collections;
using System.Collections.Generic;

namespace CurrencyApi.Application.Results
{
    public class PagedResult<T> : ResultBase
    {
        public PagedResult(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
