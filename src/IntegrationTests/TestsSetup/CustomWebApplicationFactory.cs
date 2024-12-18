using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace IntegrationTests.TestsSetup;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        _msSqlContainer.StartAsync().GetAwaiter().GetResult();
        
        builder.ConfigureTestServices(services =>
        {
            var connectionString = _msSqlContainer.GetConnectionString();
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PengaDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<PengaDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<PengaDbContext>();
            db.Database.Migrate();
        });
    }
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _msSqlContainer.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
        
        base.Dispose(disposing);
    }
}