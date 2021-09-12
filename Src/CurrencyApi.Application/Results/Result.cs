namespace CurrencyApi.Application.Results
{
    public abstract class DataResult<T> : ResultBase
    {
        public T? Data { get; set; }
    }
}
