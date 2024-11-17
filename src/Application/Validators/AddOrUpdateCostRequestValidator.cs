using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class AddOrUpdateCostRequestValidator : AbstractValidator<AddOrUpdateCostRequest>
{
    public AddOrUpdateCostRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}