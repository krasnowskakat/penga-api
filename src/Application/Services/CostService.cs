using Application.Models;
using Domain;
using FluentValidation;

namespace Application.Services;

public class CostService
{
    private readonly PengaDbContext _context;
    private readonly IValidator<AddOrUpdateCostRequest> _addOrUpdateCostRequestValidator;
    
    public CostService(PengaDbContext context, IValidator<AddOrUpdateCostRequest> addOrUpdateCostRequestValidator)
    {
        _context = context;
        _addOrUpdateCostRequestValidator = addOrUpdateCostRequestValidator;
    }
    
        
    public List<CostResponse> GetCosts()
    {
        return _context.Costs
            .Select(x => CostResponse.From(x))
            .ToList();
    }
    
    public CostResponse GetCostById(int id)
    {
        var cost = _context.Costs.Find(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        return CostResponse.From(cost);
    }
    
    public CostResponse AddCost(AddOrUpdateCostRequest request)
    {
        _addOrUpdateCostRequestValidator.ValidateAndThrow(request);

        var cost = new Cost(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        
        _context.Costs.Add(cost);
        _context.SaveChanges();
        
        return CostResponse.From(cost);
    }
    
    public CostResponse UpdateCost(int id, AddOrUpdateCostRequest request)
    {
        _addOrUpdateCostRequestValidator.ValidateAndThrow(request);
        var cost = _context.Costs.Find(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        cost.Update(request.Name, request.Description, request.Date, request.Amount, request.CategoryId);
        _context.SaveChanges();
        
        return CostResponse.From(cost);
    }
    
    public void DeleteCost(int id)
    {
        var cost = _context.Costs.Find(id);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        _context.Costs.Remove(cost);
        _context.SaveChanges();
    }

}