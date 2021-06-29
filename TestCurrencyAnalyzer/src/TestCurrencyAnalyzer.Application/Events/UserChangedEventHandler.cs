using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Common;
using TestCurrencyAnalyzer.Domain.Events;

namespace TestCurrencyAnalyzer.Application.Events
{
    public class UserChangedEventHandler : INotificationHandler<DomainEventNotification<UserChangedEvent>>
    {
        private readonly ILogger<UserChangedEventHandler> _logger;

        public UserChangedEventHandler(ILogger<UserChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<UserChangedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
