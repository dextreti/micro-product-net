using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.In;
using Catalog.Order.Kafka.Consumer;
using Catalog.Order.Kafka.Consumer.Consumers;
using Confluent.Kafka;

var builder = Host.CreateApplicationBuilder(args);

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
    GroupId = builder.Configuration["Kafka:GroupId"] ?? "order-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = true
};

builder.Services.AddSingleton(consumerConfig);
builder.Services.AddSingleton<IGenericConsumer<OrderCreatedEvent>, OrderCreatedEventConsumer>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
