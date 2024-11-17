using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class DependencyInjection
{
    public static void ConfigureServices(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PengaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}