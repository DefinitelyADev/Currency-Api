// using Microsoft.AspNetCore.Identity;

using System;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Data.Contexts
{
#nullable disable
    public class ApplicationDbContext : DbContext // IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin /*(IdentityUserLogin<string>)*/, RoleClaim, UserToken /*(IdentityUserToken<string>>)*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasDefaultSchema("currency_api");

            modelBuilder.ApplyModelBuilderConfigurations();
        }
    }
#nullable restore
}
