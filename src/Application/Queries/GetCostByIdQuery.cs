using Application.Models;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GetCostByIdQuery : IRequest<CostResponse>
{
    public int Id { get; set; }
    
    public GetCostByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetCostByIdQueryHandler : IRequestHandler<GetCostByIdQuery, CostResponse>
{
    private readonly IPengaDbContext _context;
    
    public GetCostByIdQueryHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task<CostResponse> Handle(GetCostByIdQuery request, CancellationToken cancellationToken)
    {
        var cost = await _context.Costs.FindAsync(request.Id, cancellationToken);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        return CostResponse.From(cost);
    }
}