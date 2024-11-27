using Application.Commands.Costs;
using FluentValidation;

namespace Application.Validators.Costs;

public class UpdateCostCommandValidator : AbstractValidator<UpdateCostCommand>
{
    public UpdateCostCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}