using Application.Commands.Costs;
using Application.Queries.Costs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class CostsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CostsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Returns all costs
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCosts(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCostsQuery(), cancellationToken));
    }
    
    /// <summary>
    /// Returns a cost by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCostById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCostByIdQuery(id), cancellationToken));
    }
    
    /// <summary>
    /// Adds a new cost
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> AddCost([FromBody] AddCostCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    /// <summary>
    /// Updates a cost
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateCost([FromBody] UpdateCostCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    /// <summary>
    /// Deletes a cost
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCost([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCostCommand(id), cancellationToken);
        return Ok();
    }
}