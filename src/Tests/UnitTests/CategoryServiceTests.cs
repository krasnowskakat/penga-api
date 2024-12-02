using Application.Commands.Categories;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests.UnitTests;

public class CategoryServiceTests
{
    [Fact]
    public async Task AddCategory_ShouldAddCategory_WhenCategoryIsValid()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<AddCategoryCommand>>();
        var handler = new AddCategoryCommandHandler(GetDbContext(), validatorMock.Object);
        var request = new AddCategoryCommand("Test Category");
        
        // Act
        var response = await handler.Handle(request);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
    }
    
    [Fact]
    public async Task AddCategory_ShouldThrowValidationException_WhenCategoryIsNotValid()
    {
        // Arrange
        var validator = new InlineValidator<AddCategoryCommand>();
        validator.RuleFor(x => x.Name).NotEmpty();
        
        var handler = new AddCategoryCommandHandler(GetDbContext(), validator);
        var request = new AddCategoryCommand("");
        
        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(request));
    }
    
    private IPengaDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PengaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new PengaDbContext(options);
    }
}