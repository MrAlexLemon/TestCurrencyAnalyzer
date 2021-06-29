using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;
using TestCurrencyAnalyzer.Application.Interfaces.Repositories.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Currency;
using TestCurrencyAnalyzer.Application.Queries.Currency;
using TestCurrencyAnalyzer.Domain.Entities.Currency;
using TestCurrencyAnalyzer.Infrastructure.HttpClient;

namespace TestCurrencyAnalyzer.Infrastructure.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(IHttpClientFactory httpClientFactory, ICurrencyRepository currencyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _currencyRepository = currencyRepository;
        }

        public async Task<List<ExchangeMoneyInfo>> GetCurrencyHistoryListAsync(Guid userId)
        {
            return await _currencyRepository.GetListByUserIdAsync(userId);
        }

        public async Task<PagedList<ExchangeMoneyInfo>> GetPagedCurrencyHistoryListAsync(CurrencyParameters currencyParameters)
        {
            return await _currencyRepository.GetPagedListByUserIdAsync(currencyParameters);
        }

        public async Task<ExchangeMoneyInfo> CreateCurrencyInfoAsync(Guid userId, TestCurrencyAnalyzer.Domain.Entities.Currency.Сurrency inputCurrency, 
            TestCurrencyAnalyzer.Domain.Entities.Currency.Сurrency outputCurrency, decimal amount)
        {
            var apiResponse = await HandleCurrencyInfoExchangeFromApiAsync();
            var currencyExchangeInfo = apiResponse.Where(x => x.base_ccy == inputCurrency.ToString() && x.ccy == outputCurrency.ToString()).Select(x => new { x.base_ccy, x.ccy, x.buy }).FirstOrDefault();
            
            var resultExchange = new ExchangeMoneyInfo(Guid.NewGuid(), inputCurrency, outputCurrency, amount, decimal.Parse(currencyExchangeInfo.buy, CultureInfo.InvariantCulture) * amount, userId);
            await _currencyRepository.AddAsync(resultExchange);
            return resultExchange;
        }

        private async Task<List<CurrencyResponse>> HandleCurrencyInfoExchangeFromApiAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("testCurrencyClient");
            using (var response = await httpClient.GetAsync("pubinfo?json&exchange&coursid=5", HttpCompletionOption.ResponseContentRead))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<CurrencyResponse>>(stream);
            }
        }
    }
}
