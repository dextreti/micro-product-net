using System;

namespace Catalog.Order.Postgresql.Entity;

public class PurchaseOrderItem
{
   public Guid Id { get; set; } 
    public Guid PurchaseOrderId { get; set; }
    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }    

}
