using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens
{
    public class AccessToken : JsonWebToken
    {
        public AccessToken(string token, DateTime expiration, DateTime? revoked = null) : base(token, expiration, revoked)
        {
        }
    }
}
