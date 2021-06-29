using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Domain.Exceptions.Identity
{
    public class RevokedRefreshTokenException : DomainException
    {
        public override string Code { get; } = "revoked_refresh_token";

        public RevokedRefreshTokenException() : base("Revoked refresh token.")
        {
        }
    }
}
