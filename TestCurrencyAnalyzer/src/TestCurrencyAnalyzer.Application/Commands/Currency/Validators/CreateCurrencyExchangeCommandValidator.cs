using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Commands.Currency.Validators
{
    public class CreateCurrencyExchangeCommandValidator : AbstractValidator<CreateCurrencyExchangeCommand>
    {
        public CreateCurrencyExchangeCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull().WithMessage("Email should not be null.")
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(v => v.InputСurrency)
                .NotNull().WithMessage("InputСurrency should not be null.")
                .NotEmpty().WithMessage("InputСurrency is required.");

            RuleFor(v => v.OutputСurrency)
                .NotNull().WithMessage("OutputСurrency should not be null.")
                .NotEmpty().WithMessage("OutputСurrency is required.");

            RuleFor(v => v.Amount)
                .NotNull().WithMessage("Amount should not be null.")
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0);
        }
    }
}
