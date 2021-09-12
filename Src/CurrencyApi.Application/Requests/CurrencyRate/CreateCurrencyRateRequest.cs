namespace CurrencyApi.Application.Requests.CurrencyRate
{
    public class CreateCurrencyRateRequest
    {
        public int CurrencyId {get; set;}
        public int TargetCurrencyId {get; set;}
        public decimal Rate {get;set;}
    }
}
