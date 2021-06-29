using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Infrastructure.HttpClient
{
    public class CurrencyResponse
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }
        public string sale { get; set; }
    }
}
