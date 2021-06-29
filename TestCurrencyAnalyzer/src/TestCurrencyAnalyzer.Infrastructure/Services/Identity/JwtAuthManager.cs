using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Exceptions.Identity;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Domain.Exceptions.Identity;
using TestCurrencyAnalyzer.Infrastructure.Options;

namespace TestCurrencyAnalyzer.Infrastructure.Services.Identity
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRng _rng;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig, IRng rng, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
            _rng = rng;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new Exception("Invalid token");
            }
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        public async Task<JwtAuthResultDto> GenerateTokensAsync(string username, Claim[] claims, DateTime now)
        {
            if (!(await _userRepository.ExistsAsync(username)))
            {
                throw new InvalidEmailException(username);
            }

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshTokenDto
            {
                UserName = username,
                TokenString = _rng.Generate(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };

            var user = await _userRepository.GetByNameAsync(refreshToken.UserName);
            await _refreshTokenRepository.AddAsync(new Domain.Entities.Identity.Tokens.RefreshToken(refreshToken.TokenString, refreshToken.ExpireAt, user.Id, null));

            return new JwtAuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<JwtAuthResultDto> RefreshAsync(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid token");
            }

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new Exception($"Invalid refreshToken {refreshToken}");
            }

            var userName = principal.Identity?.Name;
            if (!(await _refreshTokenRepository.ExistAsync(refreshToken)))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var existingRefreshToken = await _refreshTokenRepository.GetAsync(refreshToken);

            var user = await _userRepository.GetByNameAsync(userName);

            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }

            if (existingRefreshToken == null || existingRefreshToken.UserId != user.Id || existingRefreshToken.Expiration < now || existingRefreshToken.Revoked != null)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return await GenerateTokensAsync(userName, principal.Claims.ToArray(), now);
        }

        public async Task RevokeRefreshTokenByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new Exception($"Invalid userName {userName}");
            }

            var user = await _userRepository.GetByNameAsync(userName);

            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }

            var refreshTokens = await _refreshTokenRepository.GetActiveTokenListByUserIdAsync(user.Id);
            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.Revoke();
                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }
        }
    }
}
