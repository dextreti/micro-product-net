using System;
using Catalog.Order.Postgresql.Contexts;
using Microsoft.EntityFrameworkCore;
using Mapster;

using Catalog.Order.Domain.Ports.Out.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Postgresql.Entity;
using Catalog.Order.Domain.Dtos;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;


namespace Catalog.Order.Postgresql.Repositories.PurchaseOrders;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly OrderServiceContext _context;

    public PurchaseOrderRepository(OrderServiceContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(PurchaseOrderDomain domain, CancellationToken ct = default)
    {
        var orderEntity = domain.Adapt<PurchaseOrder>();
        await _context.PurchaseOrders.AddAsync(orderEntity, ct);
        //await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(PurchaseOrderDomain domain, CancellationToken ct = default)
    {
        // 1. Buscamos la entidad con sus hijos para que EF pueda trackear los cambios
        var existingOrder = await _context.PurchaseOrders
            .Include(x => x.PurchaseOrderItems)
            .FirstOrDefaultAsync(x => x.Id == domain.Id, ct);

        if (existingOrder is null)
            throw new InvalidOperationException($"La orden {domain.Id} no existe.");


        domain.Adapt(existingOrder);

        //await _context.SaveChangesAsync(ct);

    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {

        var order = await _context.PurchaseOrders.FindAsync(new object[] { id }, ct);
        if (order != null)
        {
            _context.PurchaseOrders.Remove(order);
        }

        // Decidi usar unitework para el guardado
        //await _context.PurchaseOrders.Where(x => x.Id == id).ExecuteDeleteAsync(ct);

    }

    public async Task<Result<PurchaseOrderResponse>> FindByIdAsync(Guid id, CancellationToken ct = default)
    {
        var domainOrder = await _context.PurchaseOrders
        .AsNoTracking()
        .ProjectToType<PurchaseOrderResponse>()
        .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (domainOrder is null)
            return Result<PurchaseOrderResponse>.Failure("Orden no encontrada.");

        return Result<PurchaseOrderResponse>.Success(domainOrder);
    }

    public async Task<Result<List<PurchaseOrderResponse>>> FindAllAsync(int page, int size, CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (size <= 0) size = 10;

        var orders = await _context.PurchaseOrders
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ProjectToType<PurchaseOrderResponse>()
            .ToListAsync(ct);

        return Result<List<PurchaseOrderResponse>>.Success(orders);
    }


}
