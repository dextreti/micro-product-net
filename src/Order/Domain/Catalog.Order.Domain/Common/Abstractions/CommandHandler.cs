using System;

namespace Catalog.Order.Domain.Common.Abstractions;

public abstract class CommandHandler<TCommand, TResponse>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected CommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<TResponse>> ExecuteAsync(TCommand command, CancellationToken ct)
    {   
        var result = await HandleInternalAsync(command, ct);

        if (result.IsSuccess){
            await _unitOfWork.SaveChangesAsync(ct);
            await AfterSaveAsync(command, result.Value!, ct);
        }
        
        return result;
    }
    
    protected abstract Task<Result<TResponse>> HandleInternalAsync(TCommand command, CancellationToken ct);
    protected virtual Task AfterSaveAsync(TCommand command, TResponse response, CancellationToken ct) 
        => Task.CompletedTask;
}