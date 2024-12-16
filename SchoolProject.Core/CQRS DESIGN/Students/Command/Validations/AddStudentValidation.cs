using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.CQRS_DESIGN.Students.Command.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Services.Interfaces;
using SchoolProject.Services.IServices;

namespace SchoolProject.Core.CQRS_DESIGN.Students.Command.Validations
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddStudentValidation(IStudentServices studentServices, IDepartmentServices departmentServices, IStringLocalizer<SharedResources> stringLocalizer)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _studentServices = studentServices;
            _departmentServices = departmentServices;
            _stringLocalizer = stringLocalizer;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Name can not be null")
                .MaximumLength(10).WithMessage("Max Length is 10");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyValue} can not be null")
                .MaximumLength(10).WithMessage("{PropertyName}Max Length is 10");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department Id is required")
                .NotNull().WithMessage("{PropertyValue} can not be null");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _studentServices.IsNameArExist(key))
                .WithMessage("Name is Exist");
            RuleFor(x => x.NameEn)
                .MustAsync(async (key, CancellationToken) => !await _studentServices.IsNameEnExist(key))
                .WithMessage("Name is Exist");


            RuleFor(x => x.DepartmentId)
                .MustAsync(async (key, CancellationToken) => await _departmentServices.IsDepartmentIdExist(key))
                .WithMessage("departmentId is not Exist");
        }
    }
}
