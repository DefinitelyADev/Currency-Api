using System.Collections.Generic;

namespace CurrencyApi.Domain.Entities
{
    public class Currency
    {
        public Currency(string name, string alphabeticCode, int numericCode, short decimalDigits)
        {
            Name = name;
            AlphabeticCode = alphabeticCode;
            NumericCode = numericCode;
            DecimalDigits = decimalDigits;
        }

        public Currency(int id, string name, string alphabeticCode, int numericCode, short decimalDigits)
        {
            Id = id;
            Name = name;
            AlphabeticCode = alphabeticCode;
            NumericCode = numericCode;
            DecimalDigits = decimalDigits;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AlphabeticCode { get; set; }
        public int NumericCode { get; set; }
        public short DecimalDigits { get; set; }
    }
}
