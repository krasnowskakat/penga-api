using Application;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CostsController : ControllerBase
{
    private readonly CostService _costService;
    public CostsController(CostService costService)
    {
        _costService = costService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCosts(CancellationToken cancellationToken)
    {
        return Ok(await _costService.GetCostsAsync(cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCostById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _costService.GetCostByIdAsync(id, cancellationToken));
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