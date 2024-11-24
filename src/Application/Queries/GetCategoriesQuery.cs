using Application.Models;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GetCategoriesQuery : IRequest<List<CategoryResponse>>
{
    
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
{
    private readonly IPengaDbContext _context;
    
    public GetCategoriesQueryHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Select(x => CategoryResponse.From(x))
            .ToListAsync(cancellationToken);
    }
}