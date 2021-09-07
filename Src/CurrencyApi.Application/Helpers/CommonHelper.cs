using CurrencyApi.Application.Interfaces;

namespace CurrencyApi.Application.Helpers
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public static class CommonHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the default file provider
        /// </summary>
        public static IWebAppFileProvider? DefaultFileProvider { get; set; }

        #endregion
    }
}