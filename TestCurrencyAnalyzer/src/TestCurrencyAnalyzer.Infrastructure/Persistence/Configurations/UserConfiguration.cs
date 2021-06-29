using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.Id).HasConversion<Guid>();
            builder.HasKey(t => t.Id);

            builder.Ignore(e => e.Events);

            builder.Property(t => t.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
