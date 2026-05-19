using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Dtos;
using Catalog.Order.Postgresql.Entity;
using Mapster;

namespace Catalog.Order.Postgresql.mapper;

public class OrderMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Domain → Entity (escritura)
        config.NewConfig<PurchaseOrderDomain, PurchaseOrder>()
            .Map(dest => dest.PurchaseOrderStatus, src => src.Status.ToString())
            .Map(dest => dest.PurchaseOrderItems, src => src.PurchaseOrderItems);

        config.NewConfig<PurchaseOrderItemDomain, PurchaseOrderItem>()
            .Ignore(dest => dest.PurchaseOrder);

        // Entity → Response (lectura)
        config.NewConfig<PurchaseOrder, PurchaseOrderResponse>()
            .Map(dest => dest.PurchaseOrderItems, src => src.PurchaseOrderItems);

        config.NewConfig<PurchaseOrderItem, PurchaseOrderItemResponse>();
    }
}
