namespace CurrencyApi.Application.Requests.CurrencyRate
{
    public class UpdateCurrencyRateRequest
    {
        public int OriginCurrencyId {get; set;}
        public int TargetCurrencyId {get; set;}
        public decimal Rate {get;set;}
    }
}
