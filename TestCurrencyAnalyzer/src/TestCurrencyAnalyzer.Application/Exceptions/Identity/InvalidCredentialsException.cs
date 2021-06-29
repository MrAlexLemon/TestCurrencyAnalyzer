using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Exceptions.Identity
{
    public class InvalidCredentialsException : AppException
    {
        public override string Code { get; } = "invalid_credentials";
        public string Email { get; }

        public InvalidCredentialsException(string email) : base("Invalid credentials.")
        {
            Email = email;
        }
    }
}
