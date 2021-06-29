using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;

namespace TestCurrencyAnalyzer.Application.Commands.Identity
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponseDto>
    {
        public string UserName { get; }
        public string Role { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        [JsonConstructor]
        public RefreshTokenCommand(string userName, string role, string accessToken, string refreshToken)
        {
            UserName = userName;
            Role = role;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }


    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly IJwtAuthManager _jwtAuthManager;

        public RefreshTokenCommandHandler(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    throw new Exception("Invalid refresh token.");
                }
                
                var jwtResult = await _jwtAuthManager.RefreshAsync(request.RefreshToken, request.AccessToken, DateTime.Now);
                
                return new RefreshTokenResponseDto(request.UserName, request.Role, jwtResult.AccessToken, jwtResult.RefreshToken.TokenString);
            }
            catch (SecurityTokenException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
