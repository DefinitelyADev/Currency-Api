using CurrencyApi.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CurrencyApi.Infrastructure.ActionFilters
{
    public class IdentityResultExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not IdentityResultException exception)
            {
                return;
            }

            context.Result = new ObjectResult(exception.Errors)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };

            context.ExceptionHandled = true;
        }

        public int Order => int.MaxValue - 9;
    }
}
