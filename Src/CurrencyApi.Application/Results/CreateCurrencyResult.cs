using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Results
{
    public class CreateCurrencyResult : DataResult<Currency>
    {
        public override Currency? Data { get; set; }
    }
}
