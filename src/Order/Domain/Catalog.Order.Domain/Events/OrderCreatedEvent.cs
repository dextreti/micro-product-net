using System;
using System.Text.Json.Serialization;
namespace Catalog.Order.Domain.Events;
public record OrderCreatedEvent(
    [property: JsonPropertyName("orderId")] string OrderId    
);


