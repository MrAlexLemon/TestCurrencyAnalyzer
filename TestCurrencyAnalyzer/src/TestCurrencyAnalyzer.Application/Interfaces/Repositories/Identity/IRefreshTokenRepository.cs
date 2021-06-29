using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens;

namespace TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task<List<RefreshToken>> GetActiveTokenListByUserIdAsync(Guid userId);
        Task<bool> ExistAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
