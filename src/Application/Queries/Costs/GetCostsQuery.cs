using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Costs;

public class GetCostsQuery : IRequest<List<CostResponse>>
{
}

public class GetCostsQueryHandler : IRequestHandler<GetCostsQuery, List<CostResponse>>
{
    private readonly IPengaDbContext _context;
    
    public GetCostsQueryHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CostResponse>> Handle(GetCostsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Costs
            .Select(x => CostResponse.From(x))
            .ToListAsync(cancellationToken);
    }
}