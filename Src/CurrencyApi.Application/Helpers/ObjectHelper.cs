using System.Diagnostics.CodeAnalysis;

namespace CurrencyApi.Application.Helpers
{
    public static class ObjectHelper
    {
        public static bool IsNull([NotNullWhen(false)] string? obj) => string.IsNullOrWhiteSpace(obj);
        public static bool IsNull([NotNullWhen(false)] object? obj) => obj == null;
    }
}
