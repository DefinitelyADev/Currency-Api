namespace CurrencyApi.Application.Requests.User
{
    public class GetUserRequest : PagedRequest
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
    }
}
