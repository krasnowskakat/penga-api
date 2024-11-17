using Domain;

namespace Application.Models;

public class CostResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public static CostResponse From(Cost cost)
    {
        return new CostResponse()
        {
            Id = cost.Id,
            Name = cost.Name
        };
    }
}