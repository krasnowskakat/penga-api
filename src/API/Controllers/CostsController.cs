using Application;
using Application.Models;
using Application.Queries;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CostsController : ControllerBase
{
    private readonly CostService _costService;
    private readonly IMediator _mediator;
    public CostsController(CostService costService, IMediator mediator)
    {
        _costService = costService;
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
    public async Task<IActionResult> AddCost([FromBody] AddOrUpdateCostRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _costService.AddCostAsync(request, cancellationToken));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCost([FromRoute] int id, [FromBody] AddOrUpdateCostRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _costService.UpdateCostAsync(id, request, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCost([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _costService.DeleteCostAsync(id, cancellationToken);
        return Ok();
    }
}