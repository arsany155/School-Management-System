using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.CQRS_DESIGN.Authorization.Command.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationRules();
            _stringLocalizer = stringLocalizer;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role Name is required")
                .NotNull().WithMessage("Role Name can not be null");
        }
    }
}
