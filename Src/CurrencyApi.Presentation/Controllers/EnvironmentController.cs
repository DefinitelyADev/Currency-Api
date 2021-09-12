using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CurrencyApi.Presentation.Controllers
{
    public class EnvironmentController : BaseApiController
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnvironmentController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IWebHostEnvironment webHostEnvironment)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("~/routes"), HttpGet]
        public IActionResult GetAllRoutes()
        {
            if (_webHostEnvironment.EnvironmentName != "Development")
                return NotFound();

            /* intentional use of var/anonymous class since this method is purely informational */
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
                .Where(ad => ad.AttributeRouteInfo != null)
                .Select(x => new
                {
                    Action = x is { RouteValues: { } } ? x.RouteValues["action"] : "n/a",
                    Controller = x is { RouteValues: { } } ? x.RouteValues["controller"] : "n/a",
                    Name = x.AttributeRouteInfo?.Name ?? "n/a",
                    Template = x.AttributeRouteInfo?.Template ?? "n/a",
                    Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First()
                }).ToList();

            return Ok(routes);
        }
    }
}
