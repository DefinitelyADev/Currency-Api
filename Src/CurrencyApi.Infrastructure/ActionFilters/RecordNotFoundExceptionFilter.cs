using CurrencyApi.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CurrencyApi.Infrastructure.ActionFilters
{
    public class RecordNotFoundExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not RecordNotFoundException exception)
            {
                return;
            }

            context.Result = new ObjectResult(exception.Message)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };

            context.ExceptionHandled = true;
        }

        public int Order => int.MaxValue - 10;
    }
}
