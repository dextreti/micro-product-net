using System;
using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.Out;
using Catalog.Order.Kafka.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Catalog.Order.Kafka;

public static class OrderKafkaProducerIoC
{

    public static IServiceCollection AddKafkaProducerIoC(this IServiceCollection services, IConfiguration configuration)
    {
        
        var producerConfig = new ProducerConfig
        {
            // "BootstrapServers" localhost:9092" o el nombre del servicio en kube
            BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092"
        };

        
        services.AddSingleton(producerConfig);
        services.AddSingleton<IGenericProducer<OrderCreatedEvent>, PurchaseOrderProducer>();

        // Registro del Consumidor como un Worker de fondo
        //services.AddHostedService<PurchaseOrderConsumerWorker>();

        return services;
    }

    
      

}
