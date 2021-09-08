namespace CurrencyApi.Application.Requests.Authentication
{
    public class RefreshTokenRequest
    {
        public RefreshTokenRequest(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
