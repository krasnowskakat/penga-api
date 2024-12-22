using Application.Models;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Categories;

public class UpdateCategoryCommand : IRequest<CategoryResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdateCategoryCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<UpdateCategoryCommand> _updateCategoryCommandValidator;
    public UpdateCategoryCommandHandler(IPengaDbContext context, IValidator<UpdateCategoryCommand> updateCategoryCommandValidator)
    {
        _context = context;
        _updateCategoryCommandValidator = updateCategoryCommandValidator;
    }
    
    public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
    {
        await _updateCategoryCommandValidator.ValidateAndThrowAsync(request, cancellationToken);
        var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID = {request.Id} not found");
        }
        
        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        
        return CategoryResponse.From(category);
    }
}