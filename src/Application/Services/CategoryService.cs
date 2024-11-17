using Application.Models;
using Domain;
using FluentValidation;

namespace Application.Services;

public class CategoryService
{
    private readonly PengaDbContext _context;
    private readonly IValidator<AddOrUpdateCategoryRequest> _addOrUpdateCategoryRequestValidator;
    
    public CategoryService(PengaDbContext context, IValidator<AddOrUpdateCategoryRequest> addOrUpdateCategoryRequestValidator)
    {
        _context = context;
        _addOrUpdateCategoryRequestValidator = addOrUpdateCategoryRequestValidator;
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
            throw new ValidationException("Category not found");
        }
        
        return CategoryResponse.From(category);
    }
    
    public CategoryResponse AddCategory(AddOrUpdateCategoryRequest request)
    {
        _addOrUpdateCategoryRequestValidator.ValidateAndThrow(request);
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
        _addOrUpdateCategoryRequestValidator.ValidateAndThrow(request);
        var category = _context.Categories.Find(id);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
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
            throw new ValidationException("Category not found");
        }
        
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

}