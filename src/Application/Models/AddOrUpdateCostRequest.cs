namespace Application.Models;

public class AddOrUpdateCostRequest
{ 
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly? Date { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
}