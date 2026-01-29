using System;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Dtos;
using Catalog.Order.Postgresql.Entity;
using Mapster;

namespace Catalog.Order.Postgresql.mapper;

public class OrderMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {        
       
        config.NewConfig<PurchaseOrder, PurchaseOrderResponse>()            
            .Map(dest => dest.PurchaseOrderItems, src => src.PurchaseOrderItems);
        config.NewConfig<PurchaseOrderItem, PurchaseOrderItemResponse>();
            
    }
}
