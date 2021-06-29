using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonConstructor]
        public SignInRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
