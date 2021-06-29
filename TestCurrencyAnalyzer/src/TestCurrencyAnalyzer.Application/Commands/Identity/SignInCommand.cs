using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Exceptions.Identity;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;

namespace TestCurrencyAnalyzer.Application.Commands.Identity
{
    public class SignInCommand : IRequest<SignInDto>
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInDto>
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<SignInCommandHandler> _logger;

        public SignInCommandHandler(IUserService userService, IJwtAuthManager jwtAuthManager, ILogger<SignInCommandHandler> logger)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<SignInDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if (!(await _userService.IsValidUserCredentialsAsync(request.Email, request.Password)))
            {
                throw new InvalidCredentialsException(request.Email);
            }

            var role = _userService.GetUserRole(request.Email);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = await _jwtAuthManager.GenerateTokensAsync(request.Email, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.Email}] logged in the system.");
            return new SignInDto(request.Email, role, jwtResult.AccessToken, jwtResult.RefreshToken.TokenString);
        }
    }
}
