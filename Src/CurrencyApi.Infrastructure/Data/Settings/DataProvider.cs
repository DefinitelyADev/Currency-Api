using System.Runtime.Serialization;

namespace CurrencyApi.Infrastructure.Data.Settings
{
    /// <summary>
    /// Represents data provider enumeration
    /// </summary>
    public enum DataProviders
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "")]
        Unknown,

        /// <summary>
        /// MySQL
        /// </summary>
        [EnumMember(Value = "mysql")]
        MySql,

        /// <summary>
        /// PostgreSQL
        /// </summary>
        [EnumMember(Value = "postgres")]
        Postgres,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        [EnumMember(Value = "sqlserver")]
        SqlServer
    }
}
