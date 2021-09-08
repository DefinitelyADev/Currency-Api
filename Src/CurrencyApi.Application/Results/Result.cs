namespace CurrencyApi.Application.Results
{
    public abstract class DataResult<T> : ResultBase
    {
        public abstract T? Data { get; set; }
    }
}
