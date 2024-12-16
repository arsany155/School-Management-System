using FluentValidation;
using SchoolProject.Core.CQRS_DESIGN.Students.Command.Models;
using SchoolProject.Services.IServices;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Command.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentServices _studentServices;

        public EditStudentValidator(IStudentServices studentServices)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _studentServices = studentServices;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name can not be null")
                .MaximumLength(10).WithMessage("Max Length is 10");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyValue} can not be null")
                .MaximumLength(10).WithMessage("{PropertyName}Max Length is 10");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (model, key, CancellationToken) => !await _studentServices.IsNameArExistExcludeSelf(key, model.Id))
                .WithMessage("Name is Exist");
            RuleFor(x => x.NameEn)
                .MustAsync(async (model, key, CancellationToken) => !await _studentServices.IsNameEnExistExcludeSelf(key, model.Id))
                .WithMessage("Name is Exist");
        }
    }
}
