using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Helpers;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Application.Queries.Currency
{
    public class GetCurrencyExchangeInfoQuery : QueryStringParameters, IRequest<PagedList<ExchangeMoneyInfo>>
    {
        public string Email { get; }
        
        [JsonConstructor]
        public GetCurrencyExchangeInfoQuery(string userNmae, int pageNumber, int pageSize)
        {
            Email = userNmae;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetCurrencyExchangeInfoQueryHandler : IRequestHandler<GetCurrencyExchangeInfoQuery, PagedList<ExchangeMoneyInfo>>
    {
        private readonly ICurrencyService _currencyService;
        private readonly IUserService _userService;

        public GetCurrencyExchangeInfoQueryHandler(ICurrencyService currencyService, IUserService userService)
        {
            _currencyService = currencyService;
            _userService = userService;
        }
        public async Task<PagedList<ExchangeMoneyInfo>> Handle(GetCurrencyExchangeInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetUserIdByEmailAsync(request.Email);
            return await _currencyService.GetPagedCurrencyHistoryListAsync(new CurrencyParameters { PageNumber = request.PageNumber, PageSize = request.PageSize, UserId = userId });
        }
    }
}
