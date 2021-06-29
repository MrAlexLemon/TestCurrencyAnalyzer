using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Exceptions.Identity
{
    public class InvalidEmailException : AppException
    {
        public override string Code { get; } = "invalid_email";

        public InvalidEmailException(string email) : base($"Invalid email: {email}.")
        {
        }
    }
}
