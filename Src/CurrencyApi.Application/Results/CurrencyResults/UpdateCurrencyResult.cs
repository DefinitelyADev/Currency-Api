using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Results.CurrencyResults
{
    public class UpdateCurrencyResult : DataResult<Currency>
    {
        public override Currency? Data { get; set; }
    }
}
