using System;

namespace Catalog.Order.Domain.Ports.In;

public interface IGenericConsumer<TMessage>
{
    Task StartAsync(string topic, CancellationToken cancellationToken = default);
}
