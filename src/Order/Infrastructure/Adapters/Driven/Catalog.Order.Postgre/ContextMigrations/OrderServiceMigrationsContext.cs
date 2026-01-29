
using Catalog.Order.Postgresql.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Catalog.Order.Postgresql.ContextMigrations;

// public class OrderServiceMigrationsContext : IDesignTimeDbContextFactory<OrderServiceContext>
// {
//     public OrderServiceContext CreateDbContext(string[] args)
//     {
//         // 1. Localizar el archivo appsettings.json (subiendo un nivel si es necesario)
//         IConfigurationRoot configuration = new ConfigurationBuilder()
//             //.SetBasePath(Directory.GetCurrentDirectory()) 
//             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Catalog.Order.API"))    
//             .AddJsonFile("appsettings.json", optional: true)
//             .AddJsonFile("appsettings.Development.json", optional: true)
//             .Build();


        
//         var optionsBuilder = new DbContextOptionsBuilder<OrderServiceContext>();


//             var connectionString = configuration.GetConnectionString("DefaultConnection");
//             //?? Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");



//         if (string.IsNullOrWhiteSpace(connectionString))
//             throw new InvalidOperationException("Could not find 'DefaultConnection' in configuration. Ensure appsettings.json is present in the startup project's output and contains the connection string.");

//         optionsBuilder.UseNpgsql(connectionString);

//         return new OrderServiceContext(optionsBuilder.Options);
//     }

    
// }

public class OrderServiceContextFactory : IDesignTimeDbContextFactory<OrderServiceContext>
{
    public OrderServiceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderServiceContext>();
        
        // Aquí pones tu cadena de conexión temporal para migraciones
        optionsBuilder.UseNpgsql("Host=localhost;Port=15432;Database=catalogs;Username=appuser;Password=secret");

        return new OrderServiceContext(optionsBuilder.Options);
    }
}