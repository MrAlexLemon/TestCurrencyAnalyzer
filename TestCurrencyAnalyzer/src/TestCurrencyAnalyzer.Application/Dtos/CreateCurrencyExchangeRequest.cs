using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class CreateCurrencyExchangeRequest
    {
        public string InputСurrency { get; set; }
        public string OutputСurrency { get; set; }
        public decimal Amount { get; set; }

        [JsonConstructor]
        public CreateCurrencyExchangeRequest(string inputСurrency, string outputСurrency, decimal amount)
        {
            InputСurrency = inputСurrency;
            OutputСurrency = outputСurrency;
            Amount = amount;
        }
    }
}
