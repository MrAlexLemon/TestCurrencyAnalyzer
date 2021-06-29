using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class ValidationErrorDetailsDto
    {
        public int StatusCode { get; }
        public string Message { get; }
        public IDictionary<string, string[]> Errors { get; }

        [JsonConstructor]
        public ValidationErrorDetailsDto(int statusCode, string message, IDictionary<string, string[]> errors)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
