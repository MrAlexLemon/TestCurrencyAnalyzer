using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Domain.Events
{
    public class UserChangedEvent : DomainEvent
    {
        public User User { get; }

        public UserChangedEvent(User user)
        {
            User = user;
        }
    }
}
