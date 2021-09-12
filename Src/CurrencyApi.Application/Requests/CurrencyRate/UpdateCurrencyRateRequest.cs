namespace CurrencyApi.Application.Requests.CurrencyRate
{
    public class UpdateCurrencyRateRequest
    {
        public int Id { get; set; }
        public int CurrencyId {get; set;}
        public int TargetCurrencyId {get; set;}
        public decimal Rate {get;set;}
    }
}
