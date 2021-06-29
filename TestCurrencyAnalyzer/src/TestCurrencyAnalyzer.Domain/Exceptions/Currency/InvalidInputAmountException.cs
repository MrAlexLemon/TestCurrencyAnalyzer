using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Domain.Exceptions.Currency
{
    public class InvalidInputAmountException : DomainException
    {
        public override string Code { get; } = "invalid_exchangeMoneyInfo_inputAmount";
        public Guid Id { get; }
        public decimal InputAmount { get; }

        public InvalidInputAmountException(Guid id, decimal inputAmount) : base(
            $"ExchangeMoneyInfo with id: {id} has invalid inputAmount.")
        {
            Id = id;
            InputAmount = inputAmount;
        }
    }
}
