using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;
using TestCurrencyAnalyzer.Application.Queries.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Application.Interfaces.Services.Currency
{
    public interface ICurrencyService
    {
        Task<List<ExchangeMoneyInfo>> GetCurrencyHistoryListAsync(Guid userId);
        Task<PagedList<ExchangeMoneyInfo>> GetPagedCurrencyHistoryListAsync(CurrencyParameters currencyParameters);
        Task<ExchangeMoneyInfo> CreateCurrencyInfoAsync(Guid userId, TestCurrencyAnalyzer.Domain.Entities.Currency.Сurrency inputCurrency,
            TestCurrencyAnalyzer.Domain.Entities.Currency.Сurrency outputCurrency, decimal amount);
    }
}
