using Catalog.Order.Application.Common.Abstractions;
using Catalog.Order.Domain.Common.Abstractions;
using Catalog.Order.Domain.Dtos;
using Catalog.Order.Domain.Ports.Out.PurchaseOrders;
using FluentValidation;

namespace Catalog.Order.Application.UseCases.GetPurchaseOrderById;

public class GetPurchaseOrderByIdHandler : QueryHandler<GetPurchaseOrderByIdQuery, PurchaseOrderResponse>, IGetPurchaseOrderByIdUseCase
{
    private readonly IPurchaseOrderRepository _repository;
    private readonly IValidator<GetPurchaseOrderByIdQuery> _validator;

    public GetPurchaseOrderByIdHandler(
        IPurchaseOrderRepository repository,
        IValidator<GetPurchaseOrderByIdQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    protected override async Task<Result<PurchaseOrderResponse>> HandleInternalAsync(GetPurchaseOrderByIdQuery query, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(query, ct);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<PurchaseOrderResponse>.Failure(errors);
        }

        return await _repository.FindByIdAsync(query.Id, ct);
    }
}
