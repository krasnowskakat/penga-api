using Microsoft.EntityFrameworkCore;

namespace Domain;

public class PengaDbContext : DbContext
{
    public PengaDbContext(DbContextOptions<PengaDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cost> Costs { get; set; }
}