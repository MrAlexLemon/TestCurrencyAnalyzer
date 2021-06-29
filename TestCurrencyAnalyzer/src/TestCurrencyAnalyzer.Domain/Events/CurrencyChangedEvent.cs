using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Domain.Events
{
    public class CurrencyChangedEvent : DomainEvent
    {
        public ExchangeMoneyInfo ExchangeMoneyInfo { get; }

        public CurrencyChangedEvent(ExchangeMoneyInfo exchangeMoneyInfo)
        {
            ExchangeMoneyInfo = exchangeMoneyInfo;
        }
    }
}
