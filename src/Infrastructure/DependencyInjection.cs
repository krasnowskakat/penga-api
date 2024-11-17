using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectionExtensions
{
    public static void AddDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PengaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}