using System;

namespace Catalog.Order.Application.UseCases.CreatePurchaseOrders;

public record CreatePurchaseOrderCommand(
    Guid CustomerId,
    List<CreatePurchaseOrderItemCommand> Items);

public record CreatePurchaseOrderItemCommand(Guid ProductId, int Quantity, decimal UnitPrice);