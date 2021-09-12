namespace CurrencyApi.Domain.Entities
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public int OriginCurrencyId {get; set;}
        public int TargetCurrencyId {get; set;}
        public decimal Rate { get; set; }
        public Currency? OriginCurrency { get; set; }
        public Currency? TargetCurrency { get; set; }
    }
}
