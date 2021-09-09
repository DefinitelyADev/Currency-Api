using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Application.Results.UserResults
{
    public class CreateUserResult : DataResult<User>
    {
        public override User? Data { get; set; }
    }
}
