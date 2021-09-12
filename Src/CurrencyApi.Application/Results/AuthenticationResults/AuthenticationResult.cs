namespace CurrencyApi.Application.Results.AuthenticationResults
{
    public class AuthenticationResult : ResultBase
    {

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
