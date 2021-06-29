using System;
using System.Collections.Generic;
using System.Text;
using TestCurrencyAnalyzer.Domain.Common;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.User
{
    public class Role : AggregateRoot
    {
        public string Name { get; private set; }

        public List<UserRole> UserRoles { get; private set; }

        protected Role()
        {
        }

        public Role(string name, List<UserRole> userRoles = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Invalid RoleName");
            }

            Name = name;
            UserRoles = userRoles ?? new List<UserRole>();
        }
    }
}
