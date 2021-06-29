using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens;

namespace TestCurrencyAnalyzer.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(t => t.Id).HasConversion<Guid>();
            builder.HasKey(t => t.Id);

            builder.Ignore(e => e.Events);

            builder.Property(t => t.Expiration)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t => t.Token)
                .IsRequired();
        }
    }
}
