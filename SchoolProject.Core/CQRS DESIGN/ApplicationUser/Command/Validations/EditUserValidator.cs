using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Validations
{
    public class EditUserValidator : AbstractValidator<EditUserCmmand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public EditUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationRules();
            _stringLocalizer = stringLocalizer;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()//.WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Full Name can not be null")
                .MaximumLength(10).WithMessage("Max Length is 10");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User Name is required")
                .NotNull().WithMessage("User Name can not be null")
                .MaximumLength(10).WithMessage(" Max Length is 10");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email can not be null");
        }
    }
}
