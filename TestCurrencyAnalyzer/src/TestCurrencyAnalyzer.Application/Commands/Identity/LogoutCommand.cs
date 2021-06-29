using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;

namespace TestCurrencyAnalyzer.Application.Commands.Identity
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string UserName { get; }

        [JsonConstructor]
        public LogoutCommand(string userName)
        {
            UserName = userName;
        }
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger<LogoutCommandHandler> _logger;

        public LogoutCommandHandler(IJwtAuthManager jwtAuthManager, ILogger<LogoutCommandHandler> logger)
        {
            _logger = logger;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _jwtAuthManager.RevokeRefreshTokenByUserNameAsync(request.UserName);
            _logger.LogInformation($"User [{request.UserName}] logged out the system.");

            return Unit.Value;
        }
    }
}
