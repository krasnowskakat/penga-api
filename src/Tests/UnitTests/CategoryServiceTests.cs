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
    // [Fact]
    // public async Task AddCategory_ShouldAddCategory_WhenCategoryIsValid()
    // {
    //     // Arrange
    //     var _validatorMock = new Mock<IValidator<AddOrUpdateCategoryRequest>>();
    //     var _categoryService = new CategoryService(GetDbContext(), _validatorMock.Object);
    //     var request = new AddOrUpdateCategoryRequest()
    //     {
    //         Name = "Test Category"
    //     };
    //     
    //     // Act
    //     var response = await _categoryService.AddCategoryAsync(request);
    //     
    //     // Assert
    //     Assert.NotNull(response);
    //     Assert.Equal(request.Name, response.Name);
    // }
    //
    // [Fact]
    // public async Task AddCategory_ShouldThrowValidationException_WhenCategoryIsNotValid()
    // {
    //     // Arrange
    //     var validator = new InlineValidator<AddOrUpdateCategoryRequest>();
    //     validator.RuleFor(x => x.Name).NotEmpty();
    //     
    //     var _categoryService = new CategoryService(GetDbContext(), validator);
    //     var request = new AddOrUpdateCategoryRequest();
    //     
    //     // Act & Assert
    //     await Assert.ThrowsAsync<ValidationException>(async () => await _categoryService.AddCategoryAsync(request));
    // }
    //
    // private IPengaDbContext GetDbContext()
    // {
    //     var options = new DbContextOptionsBuilder<PengaDbContext>()
    //         .UseInMemoryDatabase(Guid.NewGuid().ToString())
    //         .Options;
    //     
    //     return new PengaDbContext(options);
    // }
}