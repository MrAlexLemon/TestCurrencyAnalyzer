using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;
using TestCurrencyAnalyzer.Infrastructure.Persistence;

namespace TestCurrencyAnalyzer.Infrastructure.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetAsync(Guid id)
            => await _dbContext.Users.FindAsync(id);

        public async Task<User> GetByNameAsync(string name)
            => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == name.ToLowerInvariant());

        public async Task AddAsync(User user, ApplicationRole[] userRoles)
        {
            var roleNames = userRoles.Select(r => r.ToString()).ToList();
            var roles = await _dbContext.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole(role.Id));
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == name.ToLowerInvariant());
        }
    }
}
