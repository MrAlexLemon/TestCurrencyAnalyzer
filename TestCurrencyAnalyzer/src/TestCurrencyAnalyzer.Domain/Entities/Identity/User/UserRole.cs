using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Domain.Entities.Identity.User
{
    public class UserRole
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }

        protected UserRole()
        {
        }

        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public UserRole(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}
