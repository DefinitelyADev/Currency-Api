using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace CurrencyApi.Infrastructure.Data
{
    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
    public class ApplicationMySqlSqlGenerationHelper : MySqlSqlGenerationHelper
    {
        public ApplicationMySqlSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies, IMySqlOptions options) : base(dependencies, options)
        {
        }

        /// <summary>
        /// MySQL does not support the EF Core concept of schemas.
        /// This overrides the default implementation.
        /// REF: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/1100
        /// </summary>
        protected override string GetSchemaName(string name, string schema)
        {
            return schema;
        }
    }
}
