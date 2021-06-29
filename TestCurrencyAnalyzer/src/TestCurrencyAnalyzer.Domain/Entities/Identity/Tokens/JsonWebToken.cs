using System;
using System.Collections.Generic;
using System.Text;
using TestCurrencyAnalyzer.Domain.Common;
using TestCurrencyAnalyzer.Domain.Exceptions.Identity;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens
{
    public abstract class JsonWebToken : AggregateRoot
    {
        public string Token { get; protected set; }
        public DateTime Expiration { get; protected set; }
        public DateTime? Revoked { get; protected set; }

        public JsonWebToken(string token, DateTime expiration, DateTime? revoked = null)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            /*if (expiration <= DateTime.UtcNow)
                throw new ArgumentException("Invalid expiration.");

            if (revoked != null && revoked <= DateTime.Now)
                throw new ArgumentException("Invalid revoked.");*/

            Token = token;
            Expiration = expiration;
            Revoked = revoked;
        }

        public bool IsExpired() => DateTime.UtcNow >= Expiration;

        public bool IsActive() => Revoked == null && !IsExpired();

        public void Revoke()
        {
            if (Revoked != null)
            {
                throw new RevokedRefreshTokenException();
            }

            Revoked = DateTime.UtcNow;
        }
    }
}
