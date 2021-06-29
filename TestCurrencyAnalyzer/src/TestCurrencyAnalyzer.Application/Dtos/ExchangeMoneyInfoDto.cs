using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class ExchangeMoneyInfoDto
    {
        public Guid Id { get; }
        public Сurrency InputСurrency { get; }
        public Сurrency OutputСurrency { get; }
        public decimal InputAmount { get; }
        public decimal OutputAmount { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public ExchangeMoneyInfoDto(Guid id, Сurrency inputСurrency, Сurrency outputСurrency, decimal inputAmount, decimal outputAmount, Guid userId)
        {
            Id = id;
            InputСurrency = inputСurrency;
            OutputСurrency = outputСurrency;

            InputAmount = inputAmount;
            OutputAmount = outputAmount;
            UserId = userId;
        }
    }
}
