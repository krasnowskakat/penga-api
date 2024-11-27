using Application.Models;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Queries.Categories;

public class GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public int Id { get; set; }
    
    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly IPengaDbContext _context;
    
    public GetCategoryByIdQueryHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        return CategoryResponse.From(category);
    }
}