using Application.Models;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Costs;

public class UpdateCostCommand : IRequest<CostResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly? Date { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    
    public UpdateCostCommand(int id, string name, string description, DateOnly? date, decimal amount, int? categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Date = date;
        Amount = amount;
        CategoryId = categoryId;
    }
}

public class UpdateCostCommandHandler : IRequestHandler<UpdateCostCommand, CostResponse>
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<UpdateCostCommand> _updateCostCommandValidator;
    public UpdateCostCommandHandler(IPengaDbContext context, IValidator<UpdateCostCommand> updateCostCommandValidator)
    {
        _context = context;
        _updateCostCommandValidator = updateCostCommandValidator;
    }
    
    public async Task<CostResponse> Handle(UpdateCostCommand request, CancellationToken cancellationToken)
    {
        await _updateCostCommandValidator.ValidateAndThrowAsync(request, cancellationToken);
        var cost = await _context.Costs.FindAsync(request.Id, cancellationToken);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        cost.Update(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CostResponse.From(cost);
    }
}