namespace CurrencyApi.Application.Settings
{
    public class JwtConfiguration
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
