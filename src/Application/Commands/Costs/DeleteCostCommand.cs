using Domain;
using FluentValidation;
using MediatR;

namespace Application.Commands.Costs;

public class DeleteCostCommand : IRequest
{
    public int Id { get; set; }

    public DeleteCostCommand(int id)
    {
        Id = id;
    }
}

public class DeleteCostCommandHandler : IRequestHandler<DeleteCostCommand>
{
    private readonly IPengaDbContext _context;
    public DeleteCostCommandHandler(IPengaDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteCostCommand request, CancellationToken cancellationToken = default)
    {
        var cost = await _context.Costs.FindAsync(request.Id, cancellationToken);
        
        if (cost == null)
        {
            throw new ValidationException("Cost not found");
        }
        
        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}