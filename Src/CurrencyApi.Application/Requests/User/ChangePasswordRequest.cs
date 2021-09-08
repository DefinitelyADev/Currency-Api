namespace CurrencyApi.Application.Requests.User
{
    public class ChangePasswordRequest
    {
        public ChangePasswordRequest(string password, string newPassword, string verifyPassword)
        {
            Password = password;
            NewPassword = newPassword;
            VerifyPassword = verifyPassword;
        }

        // [Required]
        public string Password { get; set; }

        // [Required]
        // [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        // [Required]
        // [Compare(nameof(NewPassword))]
        // [Display(Name = "Verify Password")]
        public string VerifyPassword { get; set; }
    }
}
