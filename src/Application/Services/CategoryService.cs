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
    
        
    public List<CategoryResponse> GetCategories()
    {
        return _context.Categories
            .Select(x => CategoryResponse.From(x))
            .ToList();
    }
    
    public CategoryResponse GetCategoryById(int id)
    {
        var category = _context.Categories.Find(id);
        
        if (category == null)
        {
            return null;
        }
        
        return CategoryResponse.From(category);
    }
    
    public CategoryResponse AddCategory(AddOrUpdateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name
        };
        
        _context.Categories.Add(category);
        _context.SaveChanges();
        
        return CategoryResponse.From(category);
    }
    
    public CategoryResponse UpdateCategory(int id, AddOrUpdateCategoryRequest request)
    {
        var category = _context.Categories.Find(id);
        
        if (category == null)
        {
            return null;
        }
        
        category.Name = request.Name;
        _context.SaveChanges();
        
        return CategoryResponse.From(category);
    }
    
    public void DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        
        if (category == null)
        {
            return;
        }
        
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

}