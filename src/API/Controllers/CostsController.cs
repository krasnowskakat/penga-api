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
    public async Task<IActionResult> GetCosts()
    {
        return Ok(await _costService.GetCostsAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCostById([FromRoute] int id)
    {
        return Ok(await _costService.GetCostByIdAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCost([FromBody] AddOrUpdateCostRequest request)
    {
        return Ok(await _costService.AddCostAsync(request));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCost([FromRoute] int id, [FromBody] AddOrUpdateCostRequest request)
    {
        return Ok(await _costService.UpdateCostAsync(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCost([FromRoute] int id)
    {
        await _costService.DeleteCostAsync(id);
        return Ok();
    }
}