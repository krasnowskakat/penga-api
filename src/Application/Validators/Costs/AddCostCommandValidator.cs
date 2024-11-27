using Application.Commands.Costs;
using FluentValidation;

namespace Application.Validators.Costs;

public class AddCostCommandValidator : AbstractValidator<AddCostCommand>
{
    public AddCostCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}