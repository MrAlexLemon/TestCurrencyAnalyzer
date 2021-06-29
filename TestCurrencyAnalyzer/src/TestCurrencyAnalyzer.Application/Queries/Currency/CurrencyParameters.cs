using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;

namespace TestCurrencyAnalyzer.Application.Queries.Currency
{
    public class CurrencyParameters : QueryStringParameters
    {
        public Guid UserId { get; set; }
    }
}
