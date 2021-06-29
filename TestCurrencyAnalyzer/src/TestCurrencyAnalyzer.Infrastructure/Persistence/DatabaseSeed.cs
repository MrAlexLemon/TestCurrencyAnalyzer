using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Infrastructure.Persistence
{
    public class DatabaseSeed
    {
        public static void Seed(ApplicationDbContext context, IPasswordService passwordService)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Count() == 0)
            {

                var roles = new List<Role>
                {
                new Role(ApplicationRole.Common.ToString()),
                new Role(ApplicationRole.Administrator.ToString())
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                var users = new List<User>
                {
                    new User(Guid.NewGuid(), "admin@admin.com", passwordService.Hash("12345678")),
                    new User(Guid.NewGuid(), "common@common.com", passwordService.Hash("12345678")),
                };

                users[0].UserRoles.Add(new UserRole(context.Roles.SingleOrDefault(r => r.Name == ApplicationRole.Administrator.ToString()).Id));

                users[1].UserRoles.Add(new UserRole(context.Roles.SingleOrDefault(r => r.Name == ApplicationRole.Common.ToString()).Id));

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
