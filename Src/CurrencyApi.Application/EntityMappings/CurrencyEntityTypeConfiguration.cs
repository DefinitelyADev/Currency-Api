using CurrencyApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyApi.Application.EntityMappings
{
    public class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AlphabeticCode).HasMaxLength(5);
            builder.Property(p => p.NumericCode).HasMaxLength(5);
            
            builder.HasIndex(p => p.AlphabeticCode).IsUnique();
            builder.HasIndex(p => p.NumericCode).IsUnique();
        }
    }
}