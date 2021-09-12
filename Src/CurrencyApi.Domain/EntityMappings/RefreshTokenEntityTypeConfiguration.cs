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
            builder.Property(p => p.Token).HasMaxLength(36).ValueGeneratedOnAdd();

            builder.Property(p => p.JwtId).HasMaxLength(36);
            builder.Property(p => p.UserId).HasMaxLength(36);
        }
    }
}
