using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Currency;
using TestCurrencyAnalyzer.Application.Interfaces.Services.Identity;
using TestCurrencyAnalyzer.Domain.Entities.Currency;

namespace TestCurrencyAnalyzer.Application.Commands.Currency
{
    public class CreateCurrencyExchangeCommand : IRequest<ExchangeMoneyInfoDto>
    {
        public string Email { get; }
        public string InputСurrency { get; }
        public string OutputСurrency { get; }
        public decimal Amount { get; }

        [JsonConstructor]
        public CreateCurrencyExchangeCommand(string useremail, string inputСurrency, string outputСurrency, decimal amount)
        {
            Email = useremail;
            InputСurrency = inputСurrency;
            OutputСurrency = outputСurrency;
            Amount = amount;
        }
    }


    public class CreateCurrencyExchangeCommandHandler : IRequestHandler<CreateCurrencyExchangeCommand, ExchangeMoneyInfoDto>
    {
        private readonly ICurrencyService _currencyService;
        private readonly IUserService _userService;
        public CreateCurrencyExchangeCommandHandler(ICurrencyService currencyService, IUserService userService)
        {
            _currencyService = currencyService;
            _userService = userService;
        }

        public async Task<ExchangeMoneyInfoDto> Handle(CreateCurrencyExchangeCommand request, CancellationToken cancellationToken)
        {

            if (!Enum.TryParse(request.InputСurrency, out Сurrency InputСurrency) || !Enum.TryParse(request.OutputСurrency, out Сurrency OutputСurrency))
                throw new Exception($"Ivalid arguments: {request.InputСurrency} . {request.OutputСurrency}");

            var userId = await _userService.GetUserIdByEmailAsync(request.Email);

            var result = await _currencyService.CreateCurrencyInfoAsync(userId, InputСurrency, OutputСurrency, request.Amount);
            return new ExchangeMoneyInfoDto(result.Id, result.InputСurrency, result.OutputСurrency, result.InputAmount, result.OutputAmount, result.UserId);
        }
    }
}
