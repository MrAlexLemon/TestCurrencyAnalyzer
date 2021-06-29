using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Interfaces.Services.Identity
{
    public interface IUserService
    {
        Task<bool> IsAnExistingUserAsync(string userName);
        Task<bool> IsValidUserCredentialsAsync(string userName, string password);
        string GetUserRole(string userName);
        Task<Guid> GetUserIdByEmailAsync(string email);
    }
}
