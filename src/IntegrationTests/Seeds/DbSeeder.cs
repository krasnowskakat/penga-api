using Domain;
using Infrastructure;

namespace IntegrationTests.Seeds;

public class DbSeeder
{
    public static void Seed(PengaDbContext dbContext)
    {
        dbContext.Categories.Add(new Category()
        {
            Name = "Bills"
        });
        dbContext.Categories.Add(new Category()
        {
            Name = "Food"
        });
        dbContext.Categories.Add(new Category()
        {
            Name = "Entertainment"
        });
        dbContext.Categories.Add(new Category()
        {
            Name = "Savings"
        });
        dbContext.SaveChanges();
    }
}