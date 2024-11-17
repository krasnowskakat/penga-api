using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PengaDbContext : DbContext, IPengaDbContext
{
    public PengaDbContext(DbContextOptions<PengaDbContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cost> Costs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PengaDbContext).Assembly);
    }
}