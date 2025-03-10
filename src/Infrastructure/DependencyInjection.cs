using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string dbType = configuration["Database:Type"] ?? string.Empty;

        services.AddDbContext<AppDbContext>(options =>
        {
            switch (dbType)
            {
                case "SQLServer":
                    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
                    break;
                case "PostgreSQL":
                    options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
                    break;
                case "MySQL":
                    options.UseMySql(configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlConnection")));
                    break;
                case "SQLite":
                    options.UseSqlite(configuration.GetConnectionString("MySqlLiteConnection"));
                    break; 
                case "InMemory":
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    break; 
                
                default: throw new Exception("Invalid database type");
            }

        });
        
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }

}
