using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _events = new List<DomainEvent>();
        public IEnumerable<DomainEvent> Events => _events;
        public Guid Id { get; protected set; }

        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }

        protected void AddEvent(DomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}
