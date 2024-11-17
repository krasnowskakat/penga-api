using Application.Models;
using Domain;

namespace Application.Services;

public class CategoryService
{
    private readonly PengaDbContext _context;
    
    public CategoryService(PengaDbContext context)
    {
        _context = context;
    }
    
    public CategoryResponse AddCategory(AddCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name
        };
        
        _context.Categories.Add(category);
        _context.SaveChanges();
        
        return CategoryResponse.From(category);
    }
}