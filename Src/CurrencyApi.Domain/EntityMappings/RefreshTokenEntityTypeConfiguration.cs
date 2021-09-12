using CurrencyApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyApi.Domain.EntityMappings
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(p => p.Token);
            builder.Property(p => p.Token).ValueGeneratedOnAdd();
        }
    }
}
