namespace Catalog.Order.Domain.Dtos;

public record class PurchaseOrderResponse
{
    public Guid Id { get; set; } 
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TotalAmount { get; set; }
    public string? PurchaseOrderStatus { get; set; }       
    
    public virtual ICollection<PurchaseOrderItemResponse> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItemResponse>();

}
