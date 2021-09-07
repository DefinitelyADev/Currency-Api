using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Please select")]
        Unknown,

        /// <summary>
        /// MySQL
        /// </summary>
        [EnumMember(Value = "mysql")]
        [Display(Name = "MySql")]
        MySql,

        /// <summary>
        /// PostgreSQL
        /// </summary>
        [EnumMember(Value = "postgres")]
        [Display(Name = "PostgreSQL")]
        Postgres,

        /// <summary>
        /// MS SQL Server
        /// </summary>
        [EnumMember(Value = "sqlserver")]
        [Display(Name = "Microsoft SQL Server")]
        SqlServer
    }
}