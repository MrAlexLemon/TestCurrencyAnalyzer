using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Application.Common
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}
