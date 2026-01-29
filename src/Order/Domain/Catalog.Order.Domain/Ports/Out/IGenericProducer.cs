using System;

namespace Catalog.Order.Domain.Ports.Out;

public interface IGenericProducer<TMessage>
{
    Task PublishAsync(string topic,TMessage message, CancellationToken cancellationToken = default);

}
