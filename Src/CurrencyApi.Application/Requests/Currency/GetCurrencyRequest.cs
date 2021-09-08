namespace CurrencyApi.Application.Requests.Currency
{
    public class GetCurrencyRequest : PagedRequest
    {
        public string? Name { get; set; }
        public string? AlphabeticCode { get; set; }
        public int? NumericCode { get; set; }
        public short? DecimalDigits { get; set; }
    }
}
