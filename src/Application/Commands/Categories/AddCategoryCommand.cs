using Application.Models;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Categories;

public class AddCategoryCommand : IRequest<CategoryResponse>
{
    public string Name { get; set; }

    public AddCategoryCommand(string name)
    {
        Name = name;
    }
}

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, CategoryResponse>
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<AddCategoryCommand> _addOrUpdateCategoryRequestValidator;
    public AddCategoryCommandHandler(IPengaDbContext context, IValidator<AddCategoryCommand> addOrUpdateCategoryRequestValidator)
    {
        _context = context;
        _addOrUpdateCategoryRequestValidator = addOrUpdateCategoryRequestValidator;
    }
    
    public async Task<CategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        await _addOrUpdateCategoryRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
        var category = new Category
        {
            Name = request.Name
        };
        
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CategoryResponse.From(category);
    }
}