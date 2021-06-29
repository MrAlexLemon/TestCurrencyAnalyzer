using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;

namespace TestCurrencyAnalyzer.Infrastructure.Services.Identity
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<IPasswordService> _passwordHasher;

        public PasswordService(IPasswordHasher<IPasswordService> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Hash(string password)
        {
            return _passwordHasher.HashPassword(this, password);
        }

        public bool IsValid(string hash, string password)
        {
            return _passwordHasher.VerifyHashedPassword(this, hash, password) != PasswordVerificationResult.Failed;
        }
    }
}
