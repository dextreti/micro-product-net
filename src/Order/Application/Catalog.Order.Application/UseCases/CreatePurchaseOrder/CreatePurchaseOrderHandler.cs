using System;
using Catalog.Order.Domain.Aggregates.PurchaseOrders;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Events;
using Catalog.Order.Domain.Ports.Out;
using Catalog.Order.Domain.Ports.Out.PurchaseOrders;
using FluentValidation;

namespace Catalog.Order.Application.UseCases.CreatePurchaseOrders;

public class CreatePurchaseOrderHandler : CommandHandler<CreatePurchaseOrderCommand, Guid>
{
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IGenericProducer<OrderCreatedEvent> _producer;
    private readonly IValidator<CreatePurchaseOrderCommand> _validator;
    
    public CreatePurchaseOrderHandler(
        IUnitOfWork unitOfWork,
        IPurchaseOrderRepository purchaseOrderRepository,
        IGenericProducer<OrderCreatedEvent> producer,
        IValidator<CreatePurchaseOrderCommand> validator) : base(unitOfWork)
    {
        _purchaseOrderRepository = purchaseOrderRepository;
        _producer = producer;
        _validator = validator;        
    }

    protected async override Task<Result<Guid>> HandleInternalAsync(CreatePurchaseOrderCommand command, CancellationToken ct)
    {
        // 1. Validación
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<Guid>.Failure(errors);
        }

        // 2. Lógica de Dominio
        var orderResult = PurchaseOrderDomain.Create(command.CustomerId);
        if (orderResult.IsFailure) return Result<Guid>.Failure(orderResult.Error!);

        var purchaseOrder = orderResult.Value!;
        
        foreach (var item in command.Items)
        {
            var itemResult = purchaseOrder.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            if (itemResult.IsFailure) return Result<Guid>.Failure(itemResult.Error!);
        }

        // 3. Persistencia
        await _purchaseOrderRepository.CreateAsync(purchaseOrder, ct);
        
        // 4. Evento de Ida (Kafka)
        var @event = new OrderCreatedEvent(purchaseOrder.Id.ToString());
        await _producer.PublishAsync("purchase-order-topic", @event, ct);
        
        return Result<Guid>.Success(purchaseOrder.Id);
    }
}
