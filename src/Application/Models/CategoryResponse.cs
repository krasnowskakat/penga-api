using Domain;

namespace Application.Models;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public static CategoryResponse From(Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}