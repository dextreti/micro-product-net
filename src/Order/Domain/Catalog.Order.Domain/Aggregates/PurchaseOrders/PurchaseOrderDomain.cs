
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Common.Enums;

namespace Catalog.Order.Domain.Aggregates.PurchaseOrders;

public sealed class PurchaseOrderDomain : RootDomain
{       
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int TotalAmount { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }   
    
    private readonly List<PurchaseOrderItemDomain> _purchaseOrderItems = new();
    public IReadOnlyCollection<PurchaseOrderItemDomain> PurchaseOrderItems => _purchaseOrderItems.AsReadOnly();
    

  private PurchaseOrderDomain(Guid customerId)
    {
        Id = Guid.NewGuid();        
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = PurchaseOrderStatus.Pending;
        TotalAmount = 0;
    }

public static Result<PurchaseOrderDomain> Create(Guid customerId)
{
    
    if (customerId == Guid.Empty) return Result<PurchaseOrderDomain>.Failure("Cliente inválido.");

    var order = new PurchaseOrderDomain(customerId);    
    
    return Result<PurchaseOrderDomain>.Success(order);
}

public Result<bool> AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (_purchaseOrderItems.Any(x => x.ProductId == productId))
            return Result<bool>.Failure("El producto ya existe en la orden.");

        var itemResult = PurchaseOrderItemDomain.Create(Id, productId, quantity, unitPrice);

        if (itemResult.IsFailure)
            return Result<bool>.Failure(itemResult.Error!);

        _purchaseOrderItems.Add(itemResult.Value!);
        
        // Actualizamos el estado del agregado
        TotalAmount = _purchaseOrderItems.Sum(x => x.Quantity);
        
        return Result<bool>.Success(true);
    }

}

