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
    public IActionResult GetCosts()
    {
        return Ok(_costService.GetCosts());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetCostById([FromRoute] int id)
    {
        return Ok(_costService.GetCostById(id));
    }
    
    [HttpPost]
    public IActionResult AddCost([FromBody] AddOrUpdateCostRequest request)
    {
        return Ok(_costService.AddCost(request));
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateCost([FromRoute] int id, [FromBody] AddOrUpdateCostRequest request)
    {
        return Ok(_costService.UpdateCost(id, request));
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCost([FromRoute] int id)
    {
        _costService.DeleteCost(id);
        return Ok();
    }
}