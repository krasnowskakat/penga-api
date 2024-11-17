using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class AddOrUpdateCategoryRequestValidator : AbstractValidator<AddOrUpdateCategoryRequest>
{
    public AddOrUpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}