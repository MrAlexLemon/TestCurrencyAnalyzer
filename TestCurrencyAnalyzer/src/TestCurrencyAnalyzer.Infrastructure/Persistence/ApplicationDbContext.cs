using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Services;
using TestCurrencyAnalyzer.Domain.Common;
using TestCurrencyAnalyzer.Domain.Entities.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventService _domainEventService;

        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExchangeMoneyInfo> ExchangeMoneyInfos { get; set; }

        public ApplicationDbContext(IDomainEventService domainEventService, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _domainEventService = domainEventService;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AggregateRoot> entry in ChangeTracker.Entries<AggregateRoot>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<AggregateRoot>()
                    .Select(x => x.Entity.Events)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
