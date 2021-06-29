using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Currency;
using TestCurrencyAnalyzer.Application.Queries.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Currency;
using TestCurrencyAnalyzer.Infrastructure.Persistence;

namespace TestCurrencyAnalyzer.Infrastructure.Repositories.Currency
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CurrencyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ExchangeMoneyInfo exchangeMoneyInfo)
        {
            await _dbContext.ExchangeMoneyInfos.AddAsync(exchangeMoneyInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ExchangeMoneyInfo> GetAsync(Guid id)
        {
            return await _dbContext.ExchangeMoneyInfos.FindAsync(id);
        }

        public async Task<List<ExchangeMoneyInfo>> GetListAsync()
        {
            return await _dbContext.ExchangeMoneyInfos.ToListAsync();
        }

        public async Task<List<ExchangeMoneyInfo>> GetListByUserIdAsync(Guid userId)
        {
            return await _dbContext.ExchangeMoneyInfos.Where(x=>x.UserId==userId).ToListAsync();
        }

        public async Task<PagedList<ExchangeMoneyInfo>> GetPagedListByUserIdAsync(CurrencyParameters parameters)
        {
            return await Task.FromResult(PagedList<ExchangeMoneyInfo>.ToPagedList(_dbContext.ExchangeMoneyInfos.AsNoTracking().Where(x=>x.UserId == parameters.UserId),
                parameters.PageNumber,
                parameters.PageSize));
            /*return PagedList<ExchangeMoneyInfo>.ToPagedList(_dbContext.ExchangeMoneyInfos.AsNoTracking(),
                parameters.PageNumber,
                parameters.PageSize);*/
        }
    }
}
