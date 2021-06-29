using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Queries.Currency.Validators
{
    public class GetCurrencyExchangeInfoQueryValidator : AbstractValidator<GetCurrencyExchangeInfoQuery>
    {
        public GetCurrencyExchangeInfoQueryValidator()
        {
            RuleFor(v => v.Email)
                .NotNull().WithMessage("Email should not be null.")
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(v => v.PageNumber)
                .NotNull().WithMessage("PageNumber should not be null.")
                .NotEmpty().WithMessage("PageNumber is required.");

            RuleFor(v => v.PageSize)
                .NotNull().WithMessage("PageSize should not be null.")
                .NotEmpty().WithMessage("PageSize is required.");
        }
    }
}
