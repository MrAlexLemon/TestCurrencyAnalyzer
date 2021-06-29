using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private static readonly Regex EmailRegex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private readonly IPasswordService _passwordService;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IPasswordService passwordService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public string GetUserRole(string userName)
        {
            return ApplicationRole.Common.ToString();
        }

        public async Task<bool> IsAnExistingUserAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            return await _userRepository.ExistsAsync(userName);
        }

        public async Task<bool> IsValidUserCredentialsAsync(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName) || !EmailRegex.IsMatch(userName))
            {
                return false;
            }

            var user = await _userRepository.GetByNameAsync(userName);

            if (user is null || string.IsNullOrWhiteSpace(password) || !_passwordService.IsValid(user.Password, password))
            {
                _logger.LogError($"User with email: {userName} was not found.");
                return false;
            }

            return true;
        }

        public async Task<Guid> GetUserIdByEmailAsync(string email)
        {
            var user = await _userRepository.GetByNameAsync(email);

            if (user is null)
            {
                _logger.LogError($"User with email: {email} was not found.");
                throw new Exception("User not found");
            }

            return user.Id;
        }
    }
}
