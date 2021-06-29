using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class ErrorDetailsDto
    {
        public int StatusCode { get; }
        public string Message { get; }

        [JsonConstructor]
        public ErrorDetailsDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
