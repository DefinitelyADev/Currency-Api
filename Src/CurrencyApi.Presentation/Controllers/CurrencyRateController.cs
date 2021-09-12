using System.Threading.Tasks;
using CurrencyApi.Application.Extensions;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Requests.CurrencyRate;
using CurrencyApi.Application.Responses;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyRateResults;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    public class CurrencyRateController : BaseApiController
    {
        private readonly ICurrencyRateService _currencyRateService;

        public CurrencyRateController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCurrencyRateRequest request)
        {
            PagedResult<CurrencyRate> result = await _currencyRateService.GetAsync(request);

            if (result.Succeeded)
                return Ok(result.ToResponse());

            return BadRequest(new Response<GetCurrencyRateRequest>("Could not get currency rates.").WithErrors(result.Errors).WithData(request));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            CurrencyRate result = await _currencyRateService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("{originCurrencyId:int}/{targetCurrencyId:int}")]
        public async Task<IActionResult> GetByIds(int originCurrencyId, int targetCurrencyId)
        {
            CurrencyRate result = await _currencyRateService.GetByIdsAsync(originCurrencyId, targetCurrencyId);

            return Ok(result);
        }

        [HttpGet("calculate/{originCurrencyId:int}/{targetCurrencyId:int}")]
        public async Task<IActionResult> CalculateByIdsAsync(int originCurrencyId, int targetCurrencyId, [FromQuery] decimal amount)
        {
            CalculationResult result = await _currencyRateService.CalculateByIdsAsync(originCurrencyId, targetCurrencyId, amount);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyRateRequest request)
        {
            CreateCurrencyRateResult result = await _currencyRateService.CreateAsync(request);

            if (result.Data != null && result.Succeeded)
                return CreatedAtAction(nameof(GetById), new {id = result.Data.Id}, result.Data);

            return BadRequest(new Response<CreateCurrencyRateRequest>("Could not create currency.").WithErrors(result.Errors).WithData(request));

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCurrencyRateRequest request)
        {
            UpdateCurrencyRateResult result = await _currencyRateService.UpdateAsync(request);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<UpdateCurrencyRateRequest>($"Could not update currency with id {id}.").WithErrors(result.Errors).WithData(request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteCurrencyRateResult result = await _currencyRateService.DeleteAsync(id);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<object>($"Could not delete currency.").WithErrors(result.Errors).WithData(new { Id = id}));
        }
    }
}
