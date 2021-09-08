using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;

namespace CurrencyApi.Infrastructure.Data.Contexts
{
#nullable disable
    /// <summary>
    /// Represents the db context of this application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly string _defaultSchemaName;

        /// <summary>
        /// Creates an instance of the application dbContext.
        /// </summary>
        /// <param name="defaultSchemaName">The schema for this context.</param>
        /// <param name="options">The options for this context.</param>
        public ApplicationDbContext(string defaultSchemaName, DbContextOptions<ApplicationDbContext> options) : base(options) => _defaultSchemaName = defaultSchemaName;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<Currency> res = this.Currencies.Where(p => true).ToListAsync().Result;
            modelBuilder.HasDefaultSchema(_defaultSchemaName);

            modelBuilder.ApplyModelBuilderConfigurations();
        }

        private DbSet<Currency> Currencies { get; set; }
        private DbSet<RefreshToken> RefreshTokens { get; set; }
    }
#nullable restore
}
