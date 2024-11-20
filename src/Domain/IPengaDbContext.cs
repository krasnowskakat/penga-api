using Microsoft.EntityFrameworkCore;

namespace Domain;

public interface IPengaDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Cost> Costs { get; set; }
    
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}