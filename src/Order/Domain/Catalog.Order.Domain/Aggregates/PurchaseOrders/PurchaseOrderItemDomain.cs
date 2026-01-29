
using Catalog.Order.Domain.Common.Abstractions;

namespace Catalog.Order.Domain.Aggregates.PurchaseOrders;

public sealed class PurchaseOrderItemDomain : RootDomain
{
    
    public Guid PurchaseOrderId { get; private set; }    
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;
    

    private PurchaseOrderItemDomain(Guid purchaseOrderId, Guid productId, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        PurchaseOrderId = purchaseOrderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static Result<PurchaseOrderItemDomain> Create(Guid orderId, Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            return Result<PurchaseOrderItemDomain>.Failure("La cantidad debe ser mayor a cero.");

        if (unitPrice < 0)
            return Result<PurchaseOrderItemDomain>.Failure("El precio no puede ser negativo.");

        return Result<PurchaseOrderItemDomain>.Success(new PurchaseOrderItemDomain(orderId, productId, quantity, unitPrice));
    }
    
}