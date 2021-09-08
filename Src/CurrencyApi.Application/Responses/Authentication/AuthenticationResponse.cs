namespace CurrencyApi.Application.Responses.Authentication
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
