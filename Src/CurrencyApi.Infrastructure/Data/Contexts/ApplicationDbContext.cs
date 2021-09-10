using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Data.Contexts
{
#nullable disable
    /// <summary>
    /// Represents the db context of this application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Creates an instance of the application dbContext.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.HasDefaultSchema(_defaultSchemaName);

            modelBuilder.ApplyModelBuilderConfigurations();
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
#nullable restore
}
