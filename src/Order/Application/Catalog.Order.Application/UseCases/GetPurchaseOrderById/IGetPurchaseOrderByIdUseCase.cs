using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Dtos;

namespace Catalog.Order.Application.UseCases.GetPurchaseOrderById;

public interface IGetPurchaseOrderByIdUseCase
{
    Task<Result<PurchaseOrderResponse>> ExecuteAsync(GetPurchaseOrderByIdQuery query, CancellationToken ct);
}
