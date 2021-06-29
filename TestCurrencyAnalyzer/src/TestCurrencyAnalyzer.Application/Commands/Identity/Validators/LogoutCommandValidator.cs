using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Commands.Identity.Validators
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotNull().WithMessage("UserName should not be null.")
                .NotEmpty().WithMessage("UserName is required.");
        }
    }
}
