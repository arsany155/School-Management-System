using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Validations
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationRules();
            _stringLocalizer = stringLocalizer;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Password is required")
               .NotNull().WithMessage("Password can not be null");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password can not be null");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password can not be null");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Confirm Password is required")
                .NotEmpty().WithMessage("Email Id is required")
                .NotNull().WithMessage("Email can not be null");
        }

    }
}
