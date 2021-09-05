using System.Collections.Generic;

namespace CurrencyApi.Application.Responses
{
    public class PagedResponse<T> : Response<IEnumerable<T>>
    {
        public virtual int PageNumber { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse(IEnumerable<T> data, int totalCount) : base(data)
        {
            TotalCount = totalCount;
            Succeeded = true;
        }
    }
}