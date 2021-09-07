using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CurrencyApi.Infrastructure.Data.Settings
{
    /// <summary>
    /// Represents the data settings
    /// </summary>
    public class DataSettings
    {
        #region Ctor

        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a connection string
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a data provider
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DataProviders DataProvider { get; set; }

        /// <summary>
        /// Gets or sets a raw settings
        /// </summary>
        public IDictionary<string, string> RawDataSettings { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the information is entered
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public bool IsValid => DataProvider != DataProviders.Unknown && !string.IsNullOrEmpty(ConnectionString);

        #endregion
    }
}
