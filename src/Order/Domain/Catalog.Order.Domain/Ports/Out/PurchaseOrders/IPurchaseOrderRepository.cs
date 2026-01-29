
using Catalog.Order.Domain.Common;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Dtos;

namespace Catalog.Order.Domain.Ports.Out.PurchaseOrders;

public interface IPurchaseOrderRepository : IGenericRepository<PurchaseOrderDomain, PurchaseOrderResponse>
{    
    

}
