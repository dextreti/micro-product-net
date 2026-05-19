using System.Text.Json;
using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.In;
using Confluent.Kafka;

namespace Catalog.Order.Kafka.Consumer.Consumers;

public class OrderCreatedEventConsumer : IGenericConsumer<OrderCreatedEvent>
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<OrderCreatedEventConsumer> _logger;

    public OrderCreatedEventConsumer(ConsumerConfig config, ILogger<OrderCreatedEventConsumer> logger)
    {
        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _logger = logger;
    }

    public async Task StartAsync(string topic, CancellationToken cancellationToken = default)
    {
        _consumer.Subscribe(topic);
        _logger.LogInformation("Suscrito al topic: {Topic}", topic);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);
                if (result?.Message?.Value is null) continue;

                var @event = JsonSerializer.Deserialize<OrderCreatedEvent>(result.Message.Value);
                if (@event is null) continue;

                _logger.LogInformation("Orden recibida: {OrderId}", @event.OrderId);

                // TODO: aquí se delega al caso de uso correspondiente (ej. actualizar stock)
                await Task.CompletedTask;
            }
        }
        finally
        {
            _consumer.Close();
        }
    }
}
