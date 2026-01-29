using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Order.Postgresql.Entity;

public class PurchaseOrder
{   

    public Guid Id { get; set; } 
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TotalAmount { get; set; }

    [Column("Status")]    
    public string PurchaseOrderStatus { get; set; } = "Pending";
    
    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();


}
