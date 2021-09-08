using System.Collections.Generic;
using CurrencyApi.Application.Responses;
using CurrencyApi.Application.Results;

namespace CurrencyApi.Application.Extensions
{
    public static class PagedResponseExtensions
    {
        public static PagedResponse<T> ToResponse<T>(this PagedResult<T> result)
        {
            var response = new PagedResponse<T>(result.Data, result.TotalCount).WithErrors(result.Errors);
            response.Succeeded = result.Succeeded;

            return new PagedResponse<T>(result.Data, result.TotalCount).WithErrors(result.Errors);
        }

        public static PagedResponse<T> WithErrors<T>(this PagedResponse<T> response, List<string>? errors)
        {
            if (errors == null)
                return response;

            if (response.Errors == null)
                response.Errors = errors;
            else
                response.Errors.AddRange(errors);

            return response;
        }
    }
}
