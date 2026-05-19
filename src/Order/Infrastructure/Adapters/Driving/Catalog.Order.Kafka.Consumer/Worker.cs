using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.In;

namespace Catalog.Order.Kafka.Consumer;

public class Worker(IGenericConsumer<OrderCreatedEvent> consumer, ILogger<Worker> logger) : BackgroundService
{
    private const string Topic = "purchase-order-topic";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Consumer iniciado. Escuchando topic: {Topic}", Topic);
        await consumer.StartAsync(Topic, stoppingToken);
    }
}
