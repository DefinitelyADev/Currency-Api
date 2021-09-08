using System.Collections.Generic;
using CurrencyApi.Application.Responses;

namespace CurrencyApi.Application.Extensions
{
    public static class ResponseExtensions
    {
        public static Response<T> WithData<T>(this Response<T> response, T? data = default)
        {
            response.Data = data;

            return response;
        }

        public static Response<T> WithErrors<T>(this Response<T> response, List<string>? errors)
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
