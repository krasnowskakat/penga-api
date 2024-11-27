using Application.Models;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Costs;

public class AddCostCommand : IRequest<CostResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly? Date { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    
    public AddCostCommand(string name, string description, DateOnly? date, decimal amount, int? categoryId)
    {
        Name = name;
        Description = description;
        Date = date;
        Amount = amount;
        CategoryId = categoryId;
    }
}

public class AddCostCommandHandler : IRequestHandler<AddCostCommand, CostResponse>
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<AddCostCommand> _addCostRequestValidator;
    public AddCostCommandHandler(IPengaDbContext context, IValidator<AddCostCommand> addCostRequestValidator)
    {
        _context = context;
        _addCostRequestValidator = addCostRequestValidator;
    }
    
    public async Task<CostResponse> Handle(AddCostCommand request, CancellationToken cancellationToken)
    {
        await _addCostRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var cost = new Cost(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        
        await _context.Costs.AddAsync(cost, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CostResponse.From(cost);
    }
}