namespace CurrencyApi.Application.Requests.Currency
{
    public class UpdateCurrencyRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlphabeticCode { get; set; }
        public int NumericCode { get; set; }
        public short DecimalDigits { get; set; }
    }
}
