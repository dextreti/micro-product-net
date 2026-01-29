namespace Catalog.Order.Domain.Dtos;

public record class PurchaseOrderItemResponse
{

    public Guid Id { get; set; }     
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }    

}
