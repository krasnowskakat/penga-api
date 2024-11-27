using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Categories;

public class DeleteCategoryCommand : IRequest
{
    public int Id { get; set; }

    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IPengaDbContext _context;
    public DeleteCategoryCommandHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}