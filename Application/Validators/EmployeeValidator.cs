
// Application/Validators/EmployeeValidator.cs
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Department)
                .NotEmpty();
        }
    }
}