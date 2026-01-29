using System;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Dtos;

namespace Catalog.Order.Domain.Ports.Out.PurchaseOrders;

public interface IPurchaseOrderItemRepository
{ 
    //Task<Result<PurchaseOrderItemResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    //Task<Result<bool>> SaveAsync(PurchaseOrder order, CancellationToken ct = default);
}
