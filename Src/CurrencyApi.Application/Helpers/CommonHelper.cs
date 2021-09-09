using System.Text.RegularExpressions;
using CurrencyApi.Application.Interfaces.Core;

namespace CurrencyApi.Application.Helpers
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public static class CommonHelper
    {
        private const string EmailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

        //Passwords must be at least 8 characters and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*-)
        private const string PasswordExpression = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        //Usernames must be between 8 and 30 characters, start with the following: upper case (A-Z), lower case (a-z) and contain the following: upper case (A-Z), lower case (a-z), number (0-9) and an underscore character (e.g. _)
        private const string UsernameExpression = "^[A-Za-z][A-Za-z0-9_]{7,29}$";

        private static readonly Regex s_emailRegex;
        private static readonly Regex s_usernameRegex;
        private static readonly Regex s_passwordRegex;

        static CommonHelper()
        {
            s_emailRegex = new Regex(EmailExpression, RegexOptions.IgnoreCase);
            s_usernameRegex = new Regex(UsernameExpression);
            s_passwordRegex = new Regex(PasswordExpression);
        }

        /// <summary>
        /// Verifies that a string has a valid username format
        /// </summary>
        /// <param name="username">Username to verify</param>
        /// <returns>true if the string is a valid username and false if it's not</returns>
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            username = username.Trim();

            return s_usernameRegex.IsMatch(username);
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();

            return s_emailRegex.IsMatch(email);
        }

        /// <summary>
        /// Verifies that a string has a valid password format
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <returns>true if the string is a valid password and false if it's not</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            password = password.Trim();

            return s_passwordRegex.IsMatch(password);
        }

        /// <summary>
        /// Gets or sets the default file provider
        /// </summary>
        public static IWebAppFileProvider? DefaultFileProvider { get; set; }
    }
}
