using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.CQRS_DESIGN.Authentication.Command.Validators
{
    public class SigninValidator : AbstractValidator<SigninCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SigninValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationRules();
            _stringLocalizer = stringLocalizer;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User Name is required")
                .NotNull().WithMessage("User Name can not be null")
                .MaximumLength(10).WithMessage(" Max Length is 10");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password can not be null");
        }
    }
}
