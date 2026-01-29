using System;
using System.Text.Json;
using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.Out;
using Confluent.Kafka;


namespace Catalog.Order.Kafka.Producers;

public class PurchaseOrderProducer : IGenericProducer<OrderCreatedEvent>, IDisposable
{
    private readonly IProducer<string, string> _producer;
    
    // Configuramos el serializador para que sea compatible con Java (camelCase)
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public PurchaseOrderProducer(ProducerConfig config)
    {
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync(string topic, OrderCreatedEvent message, CancellationToken cancellationToken = default)
    {        
        // Serializamos usando las opciones de camelCase
        var json = JsonSerializer.Serialize(message, _jsonOptions);
                
        var kafkaMessage = new Message<string, string> 
        { 
            // Usamos OrderId como Key para asegurar que todos los eventos 
            // de una misma orden vayan a la misma partición de Kafka
            Key = message.OrderId, 
            Value = json 
        };
        
        await _producer.ProduceAsync(topic, kafkaMessage, cancellationToken);
    }

    public void Dispose()
    {
        // cerrar la conexión limpiamente y no dejar procesos colgados
        _producer?.Flush(TimeSpan.FromSeconds(10));
        _producer?.Dispose();
    }
}