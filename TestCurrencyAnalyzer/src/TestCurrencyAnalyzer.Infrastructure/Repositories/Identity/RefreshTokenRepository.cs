using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Identity.Tokens;
using TestCurrencyAnalyzer.Infrastructure.Persistence;

namespace TestCurrencyAnalyzer.Infrastructure.Repositories.Identity
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _dbContext.RefreshTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string token)
        {
            return await _dbContext.RefreshTokens.AnyAsync(x => x.Token == token);
        }

        public async Task<List<RefreshToken>> GetActiveTokenListByUserIdAsync(Guid userId)
        {
            return await _dbContext.RefreshTokens.Where(x => x.UserId == userId && x.Revoked == null).ToListAsync();
        }
    }
}
