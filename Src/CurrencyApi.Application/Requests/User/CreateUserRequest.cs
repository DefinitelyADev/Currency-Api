namespace CurrencyApi.Application.Requests.User
{
    public class CreateUserRequest
    {
        public CreateUserRequest(string username, string password, string confirmPassword)
        {
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
