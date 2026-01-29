
using FluentValidation;

namespace Catalog.Order.Application.UseCases.GetPurchaseOrderById;

public class GetPurchaseOrderByIdValidator :  AbstractValidator<GetPurchaseOrderByIdQuery>
{
    public GetPurchaseOrderByIdValidator()
    {
      RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El identificador de la orden es requerido.")
            // Validamos que no sea 00000000-0000-0000-0000-000000000000
            .NotEqual(Guid.Empty)
            .WithMessage("El identificador de la orden no puede ser un GUID vacío.");
    }
}


