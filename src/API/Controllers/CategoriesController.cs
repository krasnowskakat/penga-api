using Application.Commands.Categories;
using Application.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Returns all categories
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCategoriesQuery(), cancellationToken));
    }
    
    /// <summary>
    /// Returns a category by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken));
    }
    
    /// <summary>
    /// Adds a new category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    /// <summary>
    /// Updates a category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
    
    /// <summary>
    /// Deletes a category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return Ok();
    }
}