using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens
{
    public class RefreshToken : JsonWebToken
    {
        public Guid UserId { get; private set; }
        public User.User User { get; private set; }

        public RefreshToken(string token, DateTime expiration, Guid userId, DateTime? revoked = null) : base(token, expiration, revoked)
        {
            UserId = userId;
        }
    }
}
