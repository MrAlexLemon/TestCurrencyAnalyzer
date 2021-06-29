using System;
using System.Collections.Generic;
using System.Text;
using TestCurrencyAnalyzer.Domain.Common;
using TestCurrencyAnalyzer.Domain.Entities.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens;
using TestCurrencyAnalyzer.Domain.Events;
using TestCurrencyAnalyzer.Domain.Exceptions.Identity;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.User
{
    public class User : AggregateRoot
    {
        public string Email { get; private set; }
        public string Password { get; private set; }


        public List<ExchangeMoneyInfo> ExchangeMoneyInfos { get; private set; }
        public List<RefreshToken> RefreshTokens { get; private set; }
        public List<UserRole> UserRoles { get; private set; }

        protected User()
        {
        }

        public User(Guid id, string email, string password,
            List<ExchangeMoneyInfo> exchangeMoneyInfos = null,
            List<RefreshToken> refreshTokens = null,
            List<UserRole> userRoles = null)
        {

            Id = id;

            SetEmail(email.ToLowerInvariant());
            SetPassword(password);

            ExchangeMoneyInfos = exchangeMoneyInfos ?? new List<ExchangeMoneyInfo>();
            RefreshTokens = refreshTokens ?? new List<RefreshToken>();
            UserRoles = userRoles ?? new List<UserRole>();

            AddEvent(new UserChangedEvent(this));
        }

        public void SetPassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new InvalidUserPasswordException(Id, passwordHash);
            }
            Password = passwordHash;

            //AddEvent(new UserChangedEvent(this));
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidUserEmailException(Id, email);
            }
            Email = email;

            //AddEvent(new UserChangedEvent(this));
        }
    }
}
