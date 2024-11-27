using Application.Models;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CostService
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<AddOrUpdateCostRequest> _addOrUpdateCostRequestValidator;
    
    public CostService(IPengaDbContext context, IValidator<AddOrUpdateCostRequest> addOrUpdateCostRequestValidator)
    {
        _context = context;
        _addOrUpdateCostRequestValidator = addOrUpdateCostRequestValidator;
    }
    
    public async Task<CostResponse> AddCostAsync(AddOrUpdateCostRequest request, CancellationToken cancellationToken = default)
    {
        await _addOrUpdateCostRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var cost = new Cost(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        
        await _context.Costs.AddAsync(cost, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CostResponse.From(cost);
    }
    
    public async Task<CostResponse> UpdateCostAsync(int id, AddOrUpdateCostRequest request, CancellationToken cancellationToken = default)
    {
        await _addOrUpdateCostRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
        var cost = await _context.Costs.FindAsync(id, cancellationToken);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        cost.Update(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CostResponse.From(cost);
    }
    
    public async Task DeleteCostAsync(int id, CancellationToken cancellationToken = default)
    {
        var cost = await _context.Costs.FindAsync(id, cancellationToken);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync(cancellationToken);
    }

}