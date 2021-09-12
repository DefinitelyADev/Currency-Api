namespace CurrencyApi.Application.Requests.CurrencyRate
{
    public class GetCurrencyRateRequest : PagedRequest
    {
        public string? OriginCurrencyName { get; set; }
        public string? OriginCurrencyAlphabeticCode { get; set; }
        public int? OriginCurrencyNumericCode { get; set; }
        public string? TargetCurrencyName { get; set; }
        public string? TargetCurrencyAlphabeticCode { get; set; }
        public int? TargetCurrencyNumericCode { get; set; }
    }
}
