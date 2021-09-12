using System.Threading.Tasks;
using CurrencyApi.Application.Extensions;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Responses;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyResults;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    [Route("[controller]")]
    public class CurrencyController : BaseApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCurrencyRequest request)
        {
            PagedResult<Currency> result = await _currencyService.GetAsync(request);

            if (result.Succeeded)
                return Ok(result.ToResponse());

            return BadRequest(new Response<GetCurrencyRequest>("Could not get currency.").WithErrors(result.Errors).WithData(request));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Currency result = await _currencyService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyRequest request)
        {
            CreateCurrencyResult result = await _currencyService.CreateAsync(request);

            if (result.Data != null && result.Succeeded)
                return CreatedAtAction(nameof(GetById), new {id = result.Data.Id}, result.Data);

            return BadRequest(new Response<CreateCurrencyRequest>("Could not create currency.").WithErrors(result.Errors).WithData(request));

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCurrencyRequest request)
        {
            UpdateCurrencyResult result = await _currencyService.UpdateAsync(id, request);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<UpdateCurrencyRequest>($"Could not update currency with id {id}.").WithErrors(result.Errors).WithData(request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteCurrencyResult result = await _currencyService.DeleteAsync(id);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(new Response<object>($"Could not delete currency.").WithErrors(result.Errors).WithData(new {Id = id}));
        }
    }
}
