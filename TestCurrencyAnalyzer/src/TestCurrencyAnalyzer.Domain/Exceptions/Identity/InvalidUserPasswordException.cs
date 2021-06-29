using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Domain.Exceptions.Identity
{
    public class InvalidUserPasswordException : DomainException
    {
        public override string Code { get; } = "invalid_user_password";
        public Guid Id { get; }
        public string Password { get; }

        public InvalidUserPasswordException(Guid id, string password) : base(
            $"User with id: {id} has invalid password.")
        {
            Id = id;
            Password = password;
        }
    }
}
