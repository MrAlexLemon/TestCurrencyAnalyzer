using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Exceptions.Identity
{
    public class UserNotFoundException : AppException
    {
        public override string Code { get; } = "user_not_found";
        public string UserName { get; }

        public UserNotFoundException(string userName) : base($"User with ID: '{userName}' was not found.")
        {
            UserName = userName;
        }
    }
}
