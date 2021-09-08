namespace CurrencyApi.Application.Requests.Authentication
{
    public class RegisterRequest
    {
        public RegisterRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
