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
    public IActionResult GetCategories()
    {
        return Ok(_categoryService.GetCategories());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetCategoryById([FromRoute] int id)
    {
        return Ok(_categoryService.GetCategoryById(id));
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] AddOrUpdateCategoryRequest request)
    {
        return Ok(_categoryService.AddCategory(request));
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateCategory([FromRoute] int id, [FromBody] AddOrUpdateCategoryRequest request)
    {
        return Ok(_categoryService.UpdateCategory(id, request));
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory([FromRoute] int id)
    {
        _categoryService.DeleteCategory(id);
        return Ok();
    }
}