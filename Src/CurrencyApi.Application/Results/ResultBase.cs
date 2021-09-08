using System.Collections.Generic;

namespace CurrencyApi.Application.Results
{
    public abstract class ResultBase
    {
        public bool Succeeded { get; set; }
        public List<string>? Errors { get; set; }
    }
}
