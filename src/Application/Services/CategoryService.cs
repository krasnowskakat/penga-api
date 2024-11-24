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
    
    public async Task<CategoryResponse> AddCategoryAsync(AddOrUpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        await _addOrUpdateCategoryRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
        var category = new Category
        {
            Name = request.Name
        };
        
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return CategoryResponse.From(category);
    }
    
    public async Task<CategoryResponse> UpdateCategoryAsync(int id, AddOrUpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        await _addOrUpdateCategoryRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
        var category = await _context.Categories.FindAsync(id, cancellationToken);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        
        return CategoryResponse.From(category);
    }
    
    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories.FindAsync(id, cancellationToken);
        
        if (category == null)
        {
            throw new ValidationException("Category not found");
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }

}