using Catalog.Order.Domain.Common.Abstractions;

namespace Catalog.Order.Application.UseCases.CreatePurchaseOrders;

public interface ICreatePurchaseOrderUseCase
{
    Task<Result<Guid>> ExecuteAsync(CreatePurchaseOrderCommand command, CancellationToken ct);
}
