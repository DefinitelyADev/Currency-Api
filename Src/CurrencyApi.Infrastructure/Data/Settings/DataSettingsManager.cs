using System;
using System.Text;
using System.Text.Json;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Infrastructure.Core;

namespace CurrencyApi.Infrastructure.Data.Settings
{
    /// <summary>
    /// Represents the data settings manager
    /// </summary>
    public static class DataSettingsManager
    {
        #region Fields

        private static bool? s_databaseIsInstalled;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether database is already installed
        /// </summary>
        public static bool IsDatabaseInstalled()
        {
            s_databaseIsInstalled ??= !string.IsNullOrEmpty(LoadSettings(reloadSettings: true)?.ConnectionString);
            return s_databaseIsInstalled.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load data settings
        /// </summary>
        /// <param name="filePath">File path; pass null to use the default settings file</param>
        /// <param name="reloadSettings">Whether to reload data, if they already loaded</param>
        /// <param name="fileProvider">File provider</param>
        /// <returns>Data settings</returns>
        public static DataSettings? LoadSettings(string? filePath = null, bool reloadSettings = false, IWebAppFileProvider? fileProvider = null)
        {
            if (!reloadSettings && Singleton<DataSettings>.Instance != null)
            {
                return Singleton<DataSettings>.Instance;
            }

            fileProvider ??= CommonHelper.DefaultFileProvider;

            if (fileProvider == null)
            {
                throw new InvalidOperationException("File provider cannot be null, while attempting to load data settings.");
            }

            filePath ??= fileProvider.MapPath(DataSettingsDefaults.FilePath);

            //check whether file exists
            if (!fileProvider.FileExists(filePath))
            {
                return new DataSettings();
            }

            string text = fileProvider.ReadAllText(filePath, Encoding.UTF8);
            if (string.IsNullOrEmpty(text))
            {
                return new DataSettings();
            }

            //get data settings from the JSON file
            Singleton<DataSettings>.Instance = JsonSerializer.Deserialize<DataSettings>(text);

            return Singleton<DataSettings>.Instance;
        }

        /// <summary>
        /// Save data settings to the file
        /// </summary>
        /// <param name="settings">Data settings</param>
        /// <param name="fileProvider">File provider</param>
        public static void SaveSettings(DataSettings settings, IWebAppFileProvider fileProvider)
        {
            Singleton<DataSettings>.Instance = settings ?? throw new ArgumentNullException(nameof(settings));

            string filePath = fileProvider.MapPath(DataSettingsDefaults.FilePath);

            //create file if not exists
            fileProvider.CreateFile(filePath);

            //save data settings to the file
            string text = JsonSerializer.Serialize(Singleton<DataSettings>.Instance);
            fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Reset "database is installed" cached information
        /// </summary>
        public static void ResetCache()
        {
            s_databaseIsInstalled = null;
        }

        #endregion
    }
}
