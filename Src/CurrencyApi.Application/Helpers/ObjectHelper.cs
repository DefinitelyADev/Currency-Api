using System.Diagnostics.CodeAnalysis;

namespace CurrencyApi.Application.Helpers
{
    public static class ObjectHelper
    {
        public static bool IsNull([NotNullWhen(false)] object? obj) => obj == null;
    }
}
