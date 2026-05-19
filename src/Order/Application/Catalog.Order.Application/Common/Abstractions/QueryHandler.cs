using Catalog.Order.Domain.Common.Abstractions;

namespace Catalog.Order.Application.Common.Abstractions;

public abstract class QueryHandler<TQuery, TResponse>
{
    public Task<Result<TResponse>> ExecuteAsync(TQuery query, CancellationToken ct)
        => HandleInternalAsync(query, ct);

    protected abstract Task<Result<TResponse>> HandleInternalAsync(TQuery query, CancellationToken ct);
}
