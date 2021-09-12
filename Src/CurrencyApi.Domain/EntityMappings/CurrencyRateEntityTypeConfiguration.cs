using CurrencyApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyApi.Domain.EntityMappings
{
    public class CurrencyRateEntityTypeConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.ToTable("CurrencyRates");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => new { p.OriginCurrency, p.TargetCurrencyId }).IsUnique();

            builder.HasOne(p => p.OriginCurrency).WithMany().HasForeignKey(p => p.OriginCurrencyId);
            builder.HasOne(p => p.TargetCurrency).WithMany().HasForeignKey(p => p.TargetCurrencyId);

            builder.HasData(
                new CurrencyRate { Id = 1, TargetCurrencyId = 1, OriginCurrencyId = 2, Rate = 1.3764m },
                new CurrencyRate { Id = 1, TargetCurrencyId = 1, OriginCurrencyId = 4, Rate = 1.12079m },
                new CurrencyRate { Id = 1, TargetCurrencyId = 1, OriginCurrencyId = 6, Rate = 0.8731m },
                new CurrencyRate { Id = 1, TargetCurrencyId = 2, OriginCurrencyId = 3, Rate = 76.7200m },
                new CurrencyRate { Id = 1, TargetCurrencyId = 4, OriginCurrencyId = 2, Rate = 1.1379m },
                new CurrencyRate { Id = 1, TargetCurrencyId = 6, OriginCurrencyId = 5, Rate = 1.5648m }
            );
        }
    }
}
