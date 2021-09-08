namespace CurrencyApi.Application.Requests
{
    public class PagedRequest
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
