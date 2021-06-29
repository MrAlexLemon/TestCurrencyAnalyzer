using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Commands.Identity.Validators
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull().WithMessage("Email should not be null.")
                .NotEmpty().WithMessage("Email is required.");
        }
    }
}
