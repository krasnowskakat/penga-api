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
    
        
    public async Task<List<CostResponse>> GetCostsAsync()
    {
        return await _context.Costs
            .Select(x => CostResponse.From(x))
            .ToListAsync();
    }
    
    public async Task<CostResponse> GetCostByIdAsync(int id)
    {
        var cost = await _context.Costs.FindAsync(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        return CostResponse.From(cost);
    }
    
    public async Task<CostResponse> AddCostAsync(AddOrUpdateCostRequest request)
    {
        await _addOrUpdateCostRequestValidator.ValidateAndThrowAsync(request);

        var cost = new Cost(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        
        await _context.Costs.AddAsync(cost);
        await _context.SaveChangesAsync();
        
        return CostResponse.From(cost);
    }
    
    public async Task<CostResponse> UpdateCostAsync(int id, AddOrUpdateCostRequest request)
    {
        await _addOrUpdateCostRequestValidator.ValidateAndThrowAsync(request);
        var cost = await _context.Costs.FindAsync(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        cost.Update(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        await _context.SaveChangesAsync();
        
        return CostResponse.From(cost);
    }
    
    public async Task DeleteCostAsync(int id)
    {
        var cost = await _context.Costs.FindAsync(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync();
    }

}