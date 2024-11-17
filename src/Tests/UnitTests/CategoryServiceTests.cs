using Application.Models;
using Application.Services;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests.UnitTests;

public class CategoryServiceTests
{
    [Fact]
    public void AddCategory_ShouldAddCategory_WhenCategoryIsValid()
    {
        // Arrange
        var _validatorMock = new Mock<IValidator<AddOrUpdateCategoryRequest>>();
        var _categoryService = new CategoryService(GetDbContext(), _validatorMock.Object);
        var request = new AddOrUpdateCategoryRequest()
        {
            Name = "Test Category"
        };
        
        // Act
        var response = _categoryService.AddCategory(request);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
    }
    
    [Fact]
    public void AddCategory_ShouldThrowValidationException_WhenCategoryIsNotValid()
    {
        // Arrange
        var validator = new InlineValidator<AddOrUpdateCategoryRequest>();
        validator.RuleFor(x => x.Name).NotEmpty();
        
        var _categoryService = new CategoryService(GetDbContext(), validator);
        var request = new AddOrUpdateCategoryRequest();
        
        // Act & Assert
        Assert.Throws<ValidationException>(() => _categoryService.AddCategory(request));
    }
    
    private IPengaDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PengaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new PengaDbContext(options);
    }
}