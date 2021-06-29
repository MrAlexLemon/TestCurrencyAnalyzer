using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Domain.Common
{
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
