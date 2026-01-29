
using Catalog.Order.Application.UseCases.CreatePurchaseOrders;
using Catalog.Order.Application.UseCases.GetPurchaseOrderById;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection.Metadata;

namespace Catalog.Order.Application;

public static class ServiceCollectionApplicationIoC
{
      extension(IServiceCollection services)
        {            
            public IServiceCollection AddApplicationIoC()
            {           
                var assembly = typeof(ServiceCollectionApplicationIoC).Assembly;

                //services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
                //services.AddScoped<IValidator<GetPurchaseOrderByIdQuery>, GetPurchaseOrderByIdValidator>();

                services.AddValidatorsFromAssembly(typeof(ServiceCollectionApplicationIoC).Assembly);
                
                
                services.AddScoped<CreatePurchaseOrderHandler>();                
                services.AddScoped<GetPurchaseOrderByIdHandler>();                

                return services;
            }
        }  

}

