namespace Catalog.Order.Application.UseCases.GetPurchaseOrderById;

public record class GetPurchaseOrderByIdQuery
{
    public Guid Id { get; init; }

}
