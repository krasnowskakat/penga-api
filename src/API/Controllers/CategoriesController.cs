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
        return Ok("Categories");
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] AddCategoryRequest request)
    {
        return Ok(_categoryService.AddCategory(request));
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id)
    {
        return Ok($"Update Category {id}");
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        return Ok($"Delete Category {id}");
    }
}