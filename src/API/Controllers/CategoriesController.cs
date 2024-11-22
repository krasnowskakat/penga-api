using Application;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;
    
    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.GetCategoriesAsync(cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.GetCategoryByIdAsync(id, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddOrUpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.AddCategoryAsync(request, cancellationToken));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] AddOrUpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _categoryService.UpdateCategoryAsync(id, request, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategoryAsync(id, cancellationToken);
        return Ok();
    }
}