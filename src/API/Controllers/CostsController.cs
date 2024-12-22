using Application.Commands.Costs;
using Application.Queries.Costs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CostsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CostsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCosts(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCostsQuery(), cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCostById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCostByIdQuery(id), cancellationToken));
    }
    
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> AddCost([FromBody] AddCostCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateCost([FromBody] UpdateCostCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCost([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCostCommand(id), cancellationToken);
        return Ok();
    }
}