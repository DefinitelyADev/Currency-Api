using CurrencyApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyApi.Domain.EntityMappings
{
    public class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.AlphabeticCode).HasMaxLength(5);
            builder.Property(p => p.NumericCode).HasMaxLength(5);

            builder.HasIndex(c => c.AlphabeticCode).IsUnique();
            builder.HasIndex(c => c.NumericCode).IsUnique();

            builder.Property(c => c.AlphabeticCode).HasMaxLength(3).IsFixedLength().IsRequired();
            builder.Property(c => c.NumericCode).HasMaxLength(3).IsRequired();


            builder.HasIndex(p => p.AlphabeticCode).IsUnique();
            builder.HasIndex(p => p.NumericCode).IsUnique();

            builder.HasData(
                new Currency (1, "Euro", "EUR", 978, 2),
                new Currency (2, "United States dollar", "USD", 840, 2),
                new Currency (3, "Japanese yen", "JPY", 392, 0),
                new Currency (4, "Swiss franc", "CHF", 756, 2),
                new Currency (5, "Canadian dollar", "CAD", 124, 2),
                new Currency (6, "Pound sterling", "GBP", 826, 2)
            );
        }
    }
}
