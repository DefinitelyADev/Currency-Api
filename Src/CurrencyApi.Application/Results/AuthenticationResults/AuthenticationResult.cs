using System.Collections.Generic;

namespace CurrencyApi.Application.Results
{
    public class AuthenticationResult : ResultBase
    {

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
