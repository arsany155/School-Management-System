using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Models;
using SchoolProject.Core.Localization;


namespace SchoolProject.Core.CQRS_DESIGN.ApplicationUser.Command.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
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

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password can not be null");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Confirm Password is required")
                .NotEmpty().WithMessage("Email Id is required")
                .NotNull().WithMessage("Email can not be null");
        }
    }
}
