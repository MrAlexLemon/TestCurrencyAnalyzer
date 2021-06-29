using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Entities.Identity.User;

namespace TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetByNameAsync(string name);
        Task AddAsync(User user, ApplicationRole[] userRoles);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(string name);
    }
}
