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
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _categoryService.GetCategoriesAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        return Ok(await _categoryService.GetCategoryByIdAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddOrUpdateCategoryRequest request)
    {
        return Ok(await _categoryService.AddCategoryAsync(request));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] AddOrUpdateCategoryRequest request)
    {
        return Ok(await _categoryService.UpdateCategoryAsync(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return Ok();
    }
}