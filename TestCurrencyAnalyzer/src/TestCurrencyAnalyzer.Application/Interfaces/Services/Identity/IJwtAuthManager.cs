using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;

namespace TestCurrencyAnalyzer.Application.Interfaces.Services.Identity
{
    public interface IJwtAuthManager
    {
        Task<JwtAuthResultDto> GenerateTokensAsync(string username, Claim[] claims, DateTime now);
        Task<JwtAuthResultDto> RefreshAsync(string refreshToken, string accessToken, DateTime now);
        Task RevokeRefreshTokenByUserNameAsync(string userName);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
