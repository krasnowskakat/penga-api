namespace Domain
{
    public class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly? Date { get; set; }
        public decimal Amount { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public Cost(string name, string description, DateOnly? date, decimal amount, int? categoryId)
        {
            Name = name;
            Description = description;
            Date = date;
            Amount = amount;
            CategoryId = categoryId;
        }

        public void Update(string name, string description, DateOnly? date, decimal amount, int? categoryId)
        {
            Name = name;
            Description = description;
            Date = date;
            Amount = amount;
            CategoryId = categoryId;
        }
    }
}
