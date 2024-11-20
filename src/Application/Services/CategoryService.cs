using Application.Models;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CategoryService
{
    private readonly IPengaDbContext _context;
    private readonly IValidator<AddOrUpdateCategoryRequest> _addOrUpdateCategoryRequestValidator;
    
    public CategoryService(IPengaDbContext context, IValidator<AddOrUpdateCategoryRequest> addOrUpdateCategoryRequestValidator)
    {
        _context = context;
        _addOrUpdateCategoryRequestValidator = addOrUpdateCategoryRequestValidator;
    }
    
        
    public async Task<List<CategoryResponse>> GetCategoriesAsync()
    {
        return await _context.Categories
            .Select(x => CategoryResponse.From(x))
            .ToListAsync();
    }
    
    public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        return CategoryResponse.From(category);
    }
    
    public async Task<CategoryResponse> AddCategoryAsync(AddOrUpdateCategoryRequest request)
    {
        await _addOrUpdateCategoryRequestValidator.ValidateAndThrowAsync(request);
        var category = new Category
        {
            Name = request.Name
        };
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        return CategoryResponse.From(category);
    }
    
    public async Task<CategoryResponse> UpdateCategoryAsync(int id, AddOrUpdateCategoryRequest request)
    {
        await _addOrUpdateCategoryRequestValidator.ValidateAndThrowAsync(request);
        var category = await _context.Categories.FindAsync(id);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        category.Name = request.Name;
        await _context.SaveChangesAsync();
        
        return CategoryResponse.From(category);
    }
    
    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

}