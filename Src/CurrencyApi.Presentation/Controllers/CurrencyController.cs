using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyApi.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Currency>> Get()
        {
            await Task.Delay(1000);
            return Enumerable.Empty<Currency>();
        }
    }
}