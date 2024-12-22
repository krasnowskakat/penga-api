using Application.Commands.Categories;
using Application.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCategoriesQuery(), cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken));
    }
    
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return Ok();
    }
}