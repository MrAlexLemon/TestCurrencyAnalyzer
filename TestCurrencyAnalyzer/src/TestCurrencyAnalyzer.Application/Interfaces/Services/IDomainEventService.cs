using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Application.Interfaces.Services
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
