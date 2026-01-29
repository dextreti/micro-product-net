using System;
using Catalog.Order.Application.UseCases.CreatePurchaseOrders;
using FluentValidation;

namespace Catalog.Order.Application.UseCases.CreatePurchaseOrder;

public class CreatePurchaseOrderItemValidator : AbstractValidator<CreatePurchaseOrderItemCommand>
{
    public CreatePurchaseOrderItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El producto es obligatorio.")
            .NotEqual(Guid.Empty).WithMessage("El ID del producto no es válido.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");
    }
}
