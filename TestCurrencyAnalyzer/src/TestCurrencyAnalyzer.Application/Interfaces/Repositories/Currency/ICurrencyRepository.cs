using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;
using TestCurrencyAnalyzer.Application.Queries.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Application.Interfaces.Repositories.Currency
{
    public interface ICurrencyRepository
    {
        Task<ExchangeMoneyInfo> GetAsync(Guid id);
        Task<List<ExchangeMoneyInfo>> GetListByUserIdAsync(Guid userId);
        Task<List<ExchangeMoneyInfo>> GetListAsync();
        Task<PagedList<ExchangeMoneyInfo>> GetPagedListByUserIdAsync(CurrencyParameters parameters);
        Task AddAsync(ExchangeMoneyInfo exchangeMoneyInfo);
    }
}
