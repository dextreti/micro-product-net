using System;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;

namespace Catalog.Order.Domain.Ports.In.PurchaseOrders;

public interface ICreatePurchaseOrderUseCase
{
    Task<Result<Guid>> SaveAsync(PurchaseOrderDomain order, CancellationToken ct);

}
