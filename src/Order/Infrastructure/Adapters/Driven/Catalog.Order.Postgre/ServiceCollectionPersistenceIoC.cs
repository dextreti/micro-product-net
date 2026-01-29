using System;
using Catalog.Order.Domain.Ports.Out.PurchaseOrders;
using Catalog.Order.Postgresql.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Order.Domain.Common.Abstractions;
using Mapster;
using System.Reflection;
using Catalog.Order.Postgresql.Repositories.PurchaseOrders;

namespace Catalog.Order.Postgresql;

public static class ServiceCollectionPersistenceIoC
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistenciaIoC(IConfiguration configuration)
        {
            services.AddDbContext<OrderServiceContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<OrderServiceContext>());

            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }

    // QUITAMOS 'extension' y usamos 'static' con 'this'
    // public static IServiceCollection AddPersistenceIoC(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddDbContext<OrderServiceContext>(options =>
    //         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
    //     );

    //     services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();            
        
    //     return services;
    // }

}
