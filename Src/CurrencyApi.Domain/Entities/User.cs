using Microsoft.AspNetCore.Identity;

namespace CurrencyApi.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        public User(string username) : base(username)
        {
        }
    }
}
