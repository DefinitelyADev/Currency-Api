namespace CurrencyApi.Application.Requests.User
{
    public class ChangePasswordRequest
    {
        public ChangePasswordRequest(string password, string newPassword, string verifyPassword)
        {
            Password = password;
            NewPassword = newPassword;
            ConfirmPassword = verifyPassword;
        }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
