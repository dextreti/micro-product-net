using System;
using Catalog.Order.Application.UseCases.CreatePurchaseOrders;
using FluentValidation;

namespace Catalog.Order.Application.UseCases.CreatePurchaseOrder;

public class CreatePurchaseOrderValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("El cliente es obligatorio.")
            .NotEqual(Guid.Empty).WithMessage("El ID del cliente no es válido.");

        // Validamos que la lista no sea nula ni esté vacía
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("La orden debe tener al menos un ítem.");

        // APLICAR VALIDACIÓN A CADA ELEMENTO DE LA LISTA
        RuleForEach(x => x.Items)
            .SetValidator(new CreatePurchaseOrderItemValidator());
    }
}